using System.Linq.Expressions;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPIDemo.Infrastructure.Repositories;

public static class SpecificationEvaluator
{
    public static IQueryable<T> ApplySpecification<T>(this IQueryable<T> query, IRichSpecification<T> spec) where T : class
    {

        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }

        if (spec.Includes != null)
        {
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
        }

        if (spec.IncludeStrings != null)
        {
            query = spec.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));
        }

        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }

        if (spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        if (spec.IsPagingEnabled)
        {
            query = query.Skip(spec.Skip).Take(spec.Take);
        }

        return query;
    }
}