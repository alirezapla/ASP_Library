using System.Linq.Expressions;
using BookLibraryAPIDemo.Application.Models;

namespace BookLibraryAPIDemo.Infrastructure.Repositories;

public static class DynamicFilterBuilder
{
    public static Expression<Func<T, bool>> BuildFilter<T>(FilterParams filter)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, filter.PropertyName);
        var value = Convert.ChangeType(filter.PropertyValue, property.Type);
        var constant = Expression.Constant(value);

        Expression comparison = filter.Operator.ToLower() switch
        {
            "==" => Expression.Equal(property, constant),
            "!=" => Expression.NotEqual(property, constant),
            ">" => Expression.GreaterThan(property, constant),
            "<" => Expression.LessThan(property, constant),
            ">=" => Expression.GreaterThanOrEqual(property, constant),
            "<=" => Expression.LessThanOrEqual(property, constant),
            "contains" => Expression.Call(property, 
                typeof(string).GetMethod("Contains", new[] { typeof(string) }), 
                constant),
            "startswith" => Expression.Call(property,
                typeof(string).GetMethod("StartsWith", new[] { typeof(string) }),
                constant),
            "endswith" => Expression.Call(property,
                typeof(string).GetMethod("EndsWith", new[] { typeof(string) }),
                constant),
            _ => throw new NotSupportedException($"Operator '{filter.Operator}' is not supported")
        };

        return Expression.Lambda<Func<T, bool>>(comparison, parameter);
    }
}