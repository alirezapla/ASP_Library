using System.Linq.Expressions;
using BookLibraryAPIDemo.Application.Models;
using BookLibraryAPIDemo.Infrastructure.Interfaces;

namespace BookLibraryAPIDemo.Infrastructure.Repositories;

public class SpecificationBuilder<T>
{
    private List<Expression<Func<T, bool>>> _criteria = new();
    private List<Expression<Func<T, object>>> _includes = new();
    private List<string> _includeStrings = new();
    private Expression<Func<T, object>> _orderBy;
    private Expression<Func<T, object>> _orderByDescending;
    private int _skip;
    private int _take;
    private bool _isPagingEnabled;

    public SpecificationBuilder<T> Where(Expression<Func<T, bool>> criteria)
    {
        _criteria.Add(criteria);
        return this;
    }

    public SpecificationBuilder<T> Include(Expression<Func<T, object>> include)
    {
        _includes.Add(include);
        return this;
    }

    public SpecificationBuilder<T> Includes(List<Expression<Func<T, object>>> includes)
    {
        foreach (var include in includes)
        {
            _includes.Add(include);
        }

        return this;
    }

    public SpecificationBuilder<T> Where(FilterParams filter)
    {
        _criteria.Add(DynamicFilterBuilder.BuildFilter<T>(filter));
        return this;
    }

    public SpecificationBuilder<T> Include(string includeString)
    {
        _includeStrings.Add(includeString);
        return this;
    }

    public SpecificationBuilder<T> ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        _orderBy = orderByExpression;
        return this;
    }

    public SpecificationBuilder<T> ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
    {
        _orderByDescending = orderByDescendingExpression;
        return this;
    }

    public SpecificationBuilder<T> ApplyPaging(PaginationParams paginationParams)
    {
        _skip = (paginationParams.Number - 1) * paginationParams.Size;
        _take = paginationParams.Size;
        _isPagingEnabled = true;
        return this;
    }

    public IRichSpecification<T> Build()
    {
        var combinedCriteria = _criteria.DefaultIfEmpty(x => true)
            .Aggregate((current, next) => current.And(next));

        var spec = new Specification<T>(combinedCriteria)
        {
            Includes = _includes,
            IncludeStrings = _includeStrings,
            OrderBy = _orderBy,
            OrderByDescending = _orderByDescending,
            Skip = _skip,
            Take = _take,
            IsPagingEnabled = _isPagingEnabled
        };

        return spec;
    }
}