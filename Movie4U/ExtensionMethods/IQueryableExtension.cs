using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Movie4U.ExtensionMethods
{
    public static class IQueryableExtension
    {
        public static IQueryable<TEntity> PropertyFilter<TEntity>(this IQueryable<TEntity> source, string propertyName, object valueToMatch)
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

        public static async Task<TEntity> FirstOrDefaultByPropertyAsync<TEntity>(this IQueryable<TEntity> source, Expression<Func<TEntity, object>>[] propertySelectors, object[] valuesToMatch)
        {
            if (valuesToMatch.Length < propertySelectors.Length)
                return default(TEntity);

            var entityTypeParam = Expression.Constant(typeof(TEntity));

            var propertyNames = GetPropertyNames(propertySelectors);

            Expression conditions = Expression.Constant(true);
            for(int i = 0; i < propertySelectors.Length; i++)
                conditions = Expression.AndAlso(
                    conditions,
                    Expression.Equal(
                        Expression.PropertyOrField(
                            entityTypeParam,
                            propertyNames[i]),
                        Expression.Constant(
                            valuesToMatch[i])));
            
            var lambda = Expression.Lambda<Func<TEntity, bool>>(conditions, false);
            
            return await source.FirstOrDefaultAsync(lambda);
        }

        public static IQueryable<TEntity> IncludeMultiple<TEntity>(this IQueryable<TEntity> source, bool asSplitQuery, params Expression<Func<TEntity, object>>[] includers)
            where TEntity : class
        {
            if (includers == null)
                return source;

            if(asSplitQuery)
                return includers
                    .Aggregate(
                        source,
                        (current, includer) => current.Include(includer).AsSplitQuery());

            return includers
                .Aggregate(
                    source,
                    (current, includer) => current.Include(includer));
        }

        private static string[] GetPropertyNames<TEntity>(Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var result = new string[propertySelectors.Length];
            
            for(int i = 0; i < result.Length; i++)
            {
                var body = (MemberExpression)propertySelectors[i].Body;

                if (body is MemberExpression me)
                    result[i] = me.Member.Name;
                else if (body is UnaryExpression ue)
                    result[i] = ((MemberExpression)ue.Operand).Member.Name;
                else
                    result[i] = "null";
            }

            return result;
        }

    }
}
