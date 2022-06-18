using System;
using System.Linq;
using System.Linq.Expressions;

namespace Movie4U.EntitiesModels
{
    public interface IEntity<TEntity>
    {
        Expression<Func<TEntity, object>>[] GetIdSelectors();

        Func<IQueryable<TEntity>, IQueryable<TEntity>> GetDynamicFilter(int key)
        {
            return null;
        }

        Func<TEntity, TEntity, int> GetComparer(int key)
        {
            return null;
        }

    }
}
