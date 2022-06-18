using Movie4U.Configurations;
using Movie4U.EntitiesModels;
using System;
using System.Linq.Expressions;

namespace Movie4U.Utilities
{
    public static class GetOneConfigFactory<TEntity, TModel>
        where TEntity : EntitiesModelsBase<TEntity, TModel>, IEntity<TEntity>, new()
        where TModel : EntitiesModelsBase<TEntity, TModel>, IModel<TModel>, new()
    {
        public static Expression<Func<TEntity, object>>[] filterPropertySelectors;

        static GetOneConfigFactory()
        {
            var aux = new TEntity();
            filterPropertySelectors = aux.GetIdSelectors();
        }

        public static GetOneConfig<TEntity> New(object[] filterValuesToMatch, Expression<Func<TEntity, object>>[] includers = null, bool asSplitQuery = false)
        {
            return new GetOneConfig<TEntity>(filterPropertySelectors, filterValuesToMatch, includers, asSplitQuery);
        }

    }
}
