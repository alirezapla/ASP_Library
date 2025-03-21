namespace BookLibraryAPIDemo.Infrastructure.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetAllAsync();
        // Task<List<T>> GetAllBookAsync(ISpecification<T> spec = null);
        Task<(List<T> Items, int TotalCount)> GetAllAsync(int pageNumber, int pageSize, ISpecification<T> spec = null);
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
    }
}