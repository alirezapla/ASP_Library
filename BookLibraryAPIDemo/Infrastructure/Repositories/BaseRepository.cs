using System.Linq.Expressions;
using System.Reflection;
using BookLibraryAPIDemo.Application.Exceptions;
using BookLibraryAPIDemo.Application.Models;
using BookLibraryAPIDemo.Infrastructure.Context;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPIDemo.Infrastructure.Repositories
{
    public class BaseRepository<T>(BookLibraryContext context) : IBaseRepository<T>
        where T : class, IEntity
    {
        public async Task<T> CreateAsync(T entity)
        {
            try
            {
                await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();
                return entity;
            }

            catch (Exception ex)
            {
                if (ex.InnerException is not SqlException sqlEx || (sqlEx.Number != 2601 && sqlEx.Number != 2627))
                    throw new RepositoryException("An error occurred while creating the entity.", ex);
                var duplicateValue = ExtractDuplicateValueFromErrorMessage(sqlEx.Message);
                throw new DuplicateKeyException(
                    $"A duplicate key violation occurred. The value '{duplicateValue}' already exists.", ex);
            }
        }


        public async Task<T> DeleteAsync(T entity)
        {
            try
            {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("An error occurred while deleting the entity.", ex);
            }
        }

        public async Task<T> SoftDeleteAsync(T entity)
        {
            try
            {
                entity.IsDeleted = true;
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("An error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await context.Set<T>().AsNoTracking().Where(static e => e.IsDeleted == false).ToListAsync();
        }


        public async Task<(IReadOnlyList<T> Items, int TotalCount)> GetAllAsync(IRichSpecification<T> spec,
            SortParams sortParams = null)
        {
            var query = context.Set<T>().AsNoTracking().ApplySpecification(spec).AsQueryable();
            
            query = ApplySorting(query, sortParams);
            
            var totalCount = await query.CountAsync();
            var items = await query
                .ToListAsync();
            return (items, totalCount);
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdAsync(string id, Expression<Func<T, object>> include)
        {
            var query = context.Set<T>().Where(static e => e.IsDeleted == false).AsQueryable();
            return await query.Include(include).FirstOrDefaultAsync(arg => arg.Id == id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("An error occurred while updating the entity.", ex);
            }
        }

        private static string ExtractDuplicateValueFromErrorMessage(string errorMessage)
        {
            var startIndex = errorMessage.LastIndexOf('(') + 1;
            var endIndex = errorMessage.LastIndexOf(')');
            return errorMessage.Substring(startIndex, endIndex - startIndex);
        }

        private static IQueryable<T> ApplySorting(IQueryable<T> query, SortParams sortParams)
        {
            if (sortParams == null)
                return query;

            if (string.IsNullOrWhiteSpace(sortParams.SortBy) || !_allowedSortColumns.Contains(sortParams.SortBy))
                return query;

            var propertyInfo = typeof(T).GetProperty(sortParams.SortBy,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null)
                return query;

            return sortParams.SortDescending
                ? query.OrderByDescending(x => EF.Property<object>(x, sortParams.SortBy))
                : query.OrderBy(x => EF.Property<object>(x, sortParams.SortBy));
        }


        private static readonly HashSet<string> _allowedSortColumns = new()
        {
            "Title", "Author", "PublicationDate", "Price", "PublisherName", "Name"
        };
    }
}