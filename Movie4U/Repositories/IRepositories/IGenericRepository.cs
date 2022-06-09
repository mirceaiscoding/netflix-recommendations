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
        Task<List<TModel>> GetAllFilteredAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1, List<Func<TEntity, bool>> extraFilters = null);
        Task<List<TEntity>> GetAllDbFilteredAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1, List<Func<TEntity, bool>> extraFilters = null, bool asNoTracking = false);
        Task<List<TModel>> GetAllOrderedAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1, List<Func<TEntity, bool>> extraFilters = null);
        Task<List<TEntity>> GetAllDbOrderedAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1, List<Func<TEntity, bool>> extraFilters = null, bool asNoTracking = false);

        Task<List<TModel>> GetAllFromPageAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1, List<Func<TEntity, bool>> extraFilters = null);

        Task<List<TEntity>> GetAllDbFromPageAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1, List<Func<TEntity, bool>> extraFilters = null);

        Task<TModel> GetOneByIdAsync(object id);
        Task<TModel> GetOneByIdAsync(object id1, object id2);

        Task<TEntity> GetOneDbByIdAsync(object id);
        Task<TEntity> GetOneDbByIdAsync(object id1, object id2);

        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity[]> InsertMultipleAsync(TEntity[] entities);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(object id);

    }
}
