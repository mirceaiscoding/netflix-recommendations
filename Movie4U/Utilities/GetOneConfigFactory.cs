using Movie4U.Configurations;
using Movie4U.EntitiesModels;
using System;
using System.Linq.Expressions;

namespace Movie4U.Utilities
{
    public static class GetOneConfigFactory<TEntity, TModel>
        where TEntity : EntitiesModelsBase<TEntity, TModel>, new()
    {
        public static Expression<Func<TEntity, object>>[] filterPropertySelectors;

        static GetOneConfigFactory()
        {
            var aux = new TEntity();
            filterPropertySelectors = new Expression<Func<TEntity, object>>[aux.GetIds().Length];
            SetIdsSelectorsArray(filterPropertySelectors, aux);
        }

        public static GetOneConfig<TEntity> New(object[] filterValuesToMatch, Expression<Func<TEntity, object>>[] includers = null, bool asSplitQuery = false)
        {
            return new GetOneConfig<TEntity>(filterPropertySelectors, filterValuesToMatch, includers, asSplitQuery);
        }

        private static Expression<Func<TEntity, object>>[] SetIdsSelectorsArray(Expression<Func<TEntity, object>>[] filterPropertySelectors, TEntity entity)
        {
            int len = entity.GetIds().Length;

            for (int i = 0; i < len; i++)
                filterPropertySelectors[i] = entity => entity.GetIds()[i];

            return filterPropertySelectors;
        }

    }
}
