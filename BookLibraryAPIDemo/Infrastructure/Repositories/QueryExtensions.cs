using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPIDemo.Infrastructure.Repositories;

public static class QueryExtensions
{
    public static IQueryable<IReadOnlyDictionary<string, T>> ExcludeFields<T>(this IQueryable<T> query,
        params string[] excludedFields)
    {
        return query.Select(e =>
            typeof(T).GetProperties()
                .Where(p => !excludedFields.Contains(p.Name)) 
                .ToDictionary(p => p.Name, p => EF.Property<T>(e, p.Name))
        );
    }
}