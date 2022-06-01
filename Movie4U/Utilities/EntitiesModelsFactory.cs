using Movie4U.EntitiesModels;

namespace Movie4U.Utilities
{
    public static class EntitiesModelsFactory<TEntity, TModel>
        where TEntity : EntitiesModelsBase<TEntity, TModel>, new()
        where TModel : EntitiesModelsBase<TEntity, TModel>, new()
    {
        public static TModel getModel(TEntity entity)
        {
            TModel model = new TModel();
            model.Copy(entity);
            return model;
        }

        public static TEntity getEntity(TModel model)
        {
            TEntity entity = new TEntity();
            entity.Copy(model);
            return entity;
        }

    }
}
