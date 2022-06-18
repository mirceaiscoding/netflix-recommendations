using System;
using System.Linq;
using System.Linq.Expressions;

namespace Movie4U.EntitiesModels
{
    public interface IModel<TModel>
    {
        Expression<Func<TModel, object>>[] GetIdSelectors();

        /**<summary>
         * Gets an object containing the ids that form the primary key of the model.
         * </summary>*/
        sealed IdModel GetIds()
        {
            var idSelectors = GetIdSelectors();
            object[] ids = new object[idSelectors.Length];
            for (int i = 0; i < ids.Length; i++)
                ids[i] = idSelectors[i].Compile()((TModel)this);     // To be tested if works as expected. If it does, remove all the implementations from the classes.

            return new IdModel(ids);
        }


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
