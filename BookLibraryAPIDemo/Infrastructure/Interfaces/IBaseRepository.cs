using System.Linq.Expressions;

namespace BookLibraryAPIDemo.Infrastructure.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetAllAsync();

        // Task<List<T>> GetAllBookAsync(ISpecification<T> spec = null);
        Task<(List<T> Items, int TotalCount)> GetAllAsync(int pageNumber, int pageSize, ISpecification<T> spec = null);
        Task<T> GetByIdAsync(string id);
        Task<T> GetByIdAsync(string id, Expression<Func<T, object>> include);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
    }
}