using System.Linq.Expressions;
using BookLibraryAPIDemo.Application.Models;

namespace BookLibraryAPIDemo.Infrastructure.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetAllAsync();

        // Task<List<T>> GetAllBookAsync(ISpecification<T> spec = null);
        Task<(List<T> Items, int TotalCount)> GetAllAsync(PaginationParams paginationParams,
            ISpecification<T> spec = null,
            SortParams sortParams = null);

        Task<T> GetByIdAsync(string id);
        Task<T> GetByIdAsync(string id, Expression<Func<T, object>> include);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<T> SoftDeleteAsync(T entity);
    }
}