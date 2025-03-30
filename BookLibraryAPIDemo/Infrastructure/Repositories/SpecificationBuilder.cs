namespace BookLibraryAPIDemo.Infrastructure.Repositories;

public class SpecificationBuilder<T>
{
    private List<Expression<Func<T, bool>>> _criteria = new();
    private List<Expression<Func<T, object>>> _includes = new();

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

    public ISpecification<T> Build()
    {
        var spec = new Specification<T>(_criteria.Aggregate((x, y) => x.And(y)));
        spec.Includes.AddRange(_includes);
        return spec;
    }
}