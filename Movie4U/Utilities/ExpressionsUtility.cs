using System;
using System.Linq;
using System.Linq.Expressions;

namespace Movie4U.Utilities
{
    public static class ExpressionsUtility
    {
        public static IQueryable<TEntity> propertyFilter<TEntity>(IQueryable<TEntity> source, string propertyName, object valueToMatch)
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

    }
}
