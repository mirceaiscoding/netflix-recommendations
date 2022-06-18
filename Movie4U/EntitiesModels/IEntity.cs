using System;
using System.Linq;
using System.Linq.Expressions;

namespace Movie4U.EntitiesModels
{
    public interface IEntity<TEntity>
    {
        Expression<Func<TEntity, object>>[] GetIdSelectors();

        /**<summary>
         * Gets an object containing the ids that form the primary key of the entity.
         * </summary>*/
        sealed IdModel GetIds()
        {
            var idSelectors = GetIdSelectors();
            object[] ids = new object[idSelectors.Length];
            for (int i = 0; i < ids.Length; i++)
                ids[i] = idSelectors[i].Compile()((TEntity)this);     // To be tested if works as expected. If it does, remove all the implementations from the classes.

            return new IdModel(ids);
        }


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
