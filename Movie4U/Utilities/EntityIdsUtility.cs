using Movie4U.EntitiesModels;

namespace Movie4U.Utilities
{
    public static class EntityIdsUtility<TEntity>
        where TEntity : IEntity<TEntity>
    {
        /**<summary>
         * Gets an object containing the ids that form the primary key of the entity.
         * </summary>*/
        public static IdModel Get(TEntity entity)
        {
            var idSelectors = entity.GetIdSelectors();
            object[] ids = new object[idSelectors.Length];
            for (int i = 0; i < ids.Length; i++)
                ids[i] = idSelectors[i].Compile()(entity);

            return new IdModel(ids);
        }

    }
}
