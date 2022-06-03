using Movie4U.EntitiesModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Repositories.IRepositories
{
    public interface IGenericRepository<TEntity, TModel>
        where TEntity : EntitiesModelsBase<TEntity, TModel>
        where TModel : EntitiesModelsBase<TEntity, TModel>, new()
    {
        Task<IQueryable<TModel>> GetAllFilteredQueryableAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageNumber = 1);
        Task<IQueryable<TEntity>> GetAllDbFilteredQueryableAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageNumber = 1, bool asNoTracking = false);
        Task<List<TModel>> GetAllOrderedAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageNumber = 1);
        Task<List<TEntity>> GetAllDbOrderedAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageNumber = 1, bool asNoTracking = false);

        Task<List<TModel>> GetAllFromPageAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageNumber = 1);

        Task<List<TEntity>> GetAllDbFromPageAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageNumber = 1);

        Task<TModel> GetOneByIdAsync(object id);
        Task<TModel> GetOneByIdAsync(object id1, object id2);

        Task<TEntity> GetOneDbByIdAsync(object id);
        Task<TEntity> GetOneDbByIdAsync(object id1, object id2);

        Task<TEntity> InsertAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(object id);

    }
}
