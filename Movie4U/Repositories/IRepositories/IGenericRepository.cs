﻿using Movie4U.Configurations;
using Movie4U.EntitiesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Repositories.IRepositories
{
    public interface IGenericRepository<TEntity, TModel>
        where TEntity : EntitiesModelsBase<TEntity, TModel>
        where TModel : EntitiesModelsBase<TEntity, TModel>, new()
    {
        Task<IQueryable<TEntity>> GetAllDbFilteredAsync(GetAllConfig<TEntity> config = null, bool asNoTracking = false);
        
        Task<List<TModel>> GetAllOrderedAsync(GetAllConfig<TEntity> config = null, List<Func<TModel, bool>> extraModelFilters = null, Func<List<TModel>, Task> filler = null);
        Task<List<TEntity>> GetAllDbOrderedAsync(GetAllConfig<TEntity> config = null, bool asNoTracking = false);

        Task<List<TModel>> GetAllFromPageAsync(GetAllConfig<TEntity> config = null, List<Func<TModel, bool>> extraModelFilters = null, Func<List<TModel>, Task> filler = null);
        Task<List<TEntity>> GetAllDbFromPageAsync(GetAllConfig<TEntity> config = null);

        Task<TModel> GetOneByIdAsync(params object[] ids);
        Task<TEntity> GetOneDbByIdAsync(params object[] ids);

        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity[]> InsertMultipleAsync(TEntity[] entities);
        Task<TEntity[]> InsertOrUpdateMultipleAsync(TEntity[] entities);

        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> DeleteAsync(TEntity entity);
        Task<bool> DeleteAsync(params object[] ids);

    }
}
