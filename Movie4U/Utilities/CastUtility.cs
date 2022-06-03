﻿using Movie4U.EntitiesModels;
using System.Collections.Generic;
using System.Linq;

namespace Movie4U.Utilities
{
    public static class CastUtility
    {
        public static TModel ToModel<TEntity, TModel>(TEntity entity)
            where TEntity: EntitiesModelsBase<TEntity, TModel>
            where TModel: EntitiesModelsBase<TEntity, TModel>, new()
        {
            var model = new TModel();
            model.Copy(entity);

            return model;
        }

        public static List<TModel> ToModelsList<TEntity, TModel> (List<TEntity> entities) 
            where TEntity: EntitiesModelsBase<TEntity, TModel>
            where TModel: EntitiesModelsBase<TEntity, TModel>, new()
        { 
            var models = new List<TModel>();
            foreach (var entity in entities)
            {
                var model = new TModel();
                model.Copy(entity);

                models.Add(model);
            }

            return models;
        }

    }
}
