using System.Linq.Expressions;
using BookLibraryAPIDemo.Infrastructure.Interfaces;

namespace BookLibraryAPIDemo.Infrastructure.Repositories;

public static class SpecificationExtensions
{
    public static IRichSpecification<T> And<T>(this IRichSpecification<T> left, IRichSpecification<T> right)
    {
        var param = Expression.Parameter(typeof(T));
        var combined = Expression.AndAlso(
            Expression.Invoke(left.Criteria, param),
            Expression.Invoke(right.Criteria, param)
        );

        var combinedSpec = new Specification<T>(
            Expression.Lambda<Func<T, bool>>(combined, param)
        );

        combinedSpec.Includes.AddRange(left.Includes);
        combinedSpec.Includes.AddRange(right.Includes);

        return combinedSpec;
    }

    public static IRichSpecification<T> Or<T>(this IRichSpecification<T> left, IRichSpecification<T> right)
    {
        var param = Expression.Parameter(typeof(T));
        var combined = Expression.OrElse(
            Expression.Invoke(left.Criteria, param),
            Expression.Invoke(right.Criteria, param)
        );

        var lambda = Expression.Lambda<Func<T, bool>>(combined, param);
        return new Specification<T>(lambda);
    }
    
    
}