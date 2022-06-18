using System;
using System.Linq;
using System.Linq.Expressions;

namespace Movie4U.EntitiesModels
{
    public interface IModel<TModel>
    {
        Expression<Func<TModel, object>>[] GetIdSelectors();

        Func<IQueryable<TModel>, IQueryable<TModel>> GetDynamicFilter(int key)
        {
            return null;
        }

        Func<IQueryable<TModel>, IQueryable<TModel>> GetDynamicSorter(int key)
        {
            return null;
        }

        Func<TModel, TModel, int> GetComparer(int key)
        {
            return null;
        }
    }
}
