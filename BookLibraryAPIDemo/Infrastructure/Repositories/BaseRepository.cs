using System.Linq.Expressions;
using BookLibraryAPIDemo.Application.Exceptions;
using BookLibraryAPIDemo.Infrastructure.Context;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPIDemo.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity
    {
        private readonly BookLibraryContext _context;

        public BaseRepository(BookLibraryContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }

            catch (Exception ex)
            {
                if (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
                {
                    var duplicateValue = ExtractDuplicateValueFromErrorMessage(sqlEx.Message);
                    throw new DuplicateKeyException(
                        $"A duplicate key violation occurred. The value '{duplicateValue}' already exists.", ex);
                }

                throw new RepositoryException("An error occurred while creating the entity.", ex);
            }
        }


        public async Task<T> DeleteAsync(T entity)
        {
            try
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
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
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("An error occurred while deleting the entity.", ex);
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().Where(e=> e.IsDeleted == false).ToListAsync();
        }


        public async Task<(List<T> Items, int TotalCount)> GetAllAsync(int pageNumber = 1, int pageSize = 10,
            ISpecification<T> spec = null)
        {
            var query = _context.Set<T>().Where(e=> e.IsDeleted == false).AsQueryable();

            if (spec != null)
            {
                if (spec.Criteria != null)
                {
                    query = query.Where(spec.Criteria);
                }

                if (spec.Includes != null)
                {
                    foreach (var include in spec.Includes)
                    {
                        query = query.Include(include);
                    }
                }
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdAsync(string id, Expression<Func<T, object>> include)
        {
            var query = _context.Set<T>().AsQueryable();

            query = query.Include(include);

            return await query.FirstOrDefaultAsync(arg => arg.Id == id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
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
    }
}