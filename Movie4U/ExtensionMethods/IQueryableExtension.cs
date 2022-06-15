using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Movie4U.ExtensionMethods
{
    public static class IQueryableExtension
    {
        public static IQueryable<TEntity> propertyFilter<TEntity>(this IQueryable<TEntity> source, string propertyName, object valueToMatch)
        {
            var param = Expression.Parameter(typeof(TEntity));

            var accessor = Expression.PropertyOrField(param, propertyName);
            if (accessor == null)
                return source;

            var constant = Expression.Constant(valueToMatch);
            // should add a check if valueToMatch's type is the same as accessor's
            var equals = Expression.Equal(accessor, constant);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equals, false, param);
            return source.Where(lambda);
        }

        public static IQueryable<TEntity> IncludeMultiple<TEntity>(this IQueryable<TEntity> source, params Expression<Func<TEntity, object>>[] includers)
            where TEntity : class
        {
            if (includers == null)
                return source;

            return includers
                .Aggregate(
                    source,
                    (current, includer) => current.Include(includer));
        }

    }
}
