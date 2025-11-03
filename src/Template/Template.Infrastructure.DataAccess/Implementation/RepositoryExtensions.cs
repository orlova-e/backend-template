using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Core.Conditions;
using Template.Domain.Core.Entities;
using Template.Domain.Core.Interfaces;

namespace Template.Infrastructure.DataAccess.Implementation;

internal static class RepositoryExtensions
{
    public static IQueryable<TEntity> MatchInclude<TEntity, TKey>(this IQueryable<TEntity> source)
        where TEntity : class, IEntity<TKey>
    {
        return source switch
        {
            IQueryable<EntityBase> options => (IQueryable<TEntity>) options.IgnoreQueryFilters(),
            _ => source
        };
    }

    public static IQueryable<T> OrderBy<T>(
        this IQueryable<T> source,
        string orderByProperty,
        SortDir sortDir)
    {
        var type = typeof(T);
        var property = type.GetProperty(orderByProperty);
        var parameter = Expression.Parameter(type, "p");
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var orderByExpression = Expression.Lambda(propertyAccess, parameter);

        var orderMethodName = sortDir == SortDir.Asc ? nameof(Queryable.OrderBy) : nameof(Queryable.OrderByDescending);
        
        MethodCallExpression resultExpression = Expression.Call(
            typeof(Queryable),
            orderMethodName,
            new Type[] { type, property.PropertyType },
            source.Expression,
            Expression.Quote(orderByExpression));
        
        return source.Provider.CreateQuery<T>(resultExpression);
    }
}