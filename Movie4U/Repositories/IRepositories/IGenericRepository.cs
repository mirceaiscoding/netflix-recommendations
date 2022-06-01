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
        Task<IQueryable<TEntity>> GetAllDbQueryableAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageNumber = 1);

        List<TModel> GetAll(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageNumber = 1);
        Task<List<TModel>> GetAllAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageNumber = 1);

        IEnumerable<TEntity> GetAllDb(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageNumber = 1);
        Task<List<TEntity>> GetAllDbAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageNumber = 1);

        TModel GetOneById(object id);
        Task<TModel> GetOneByIdAsync(object id);
        TModel GetOneById(object id1, object id2);
        Task<TModel> GetOneByIdAsync(object id1, object id2);

        TEntity GetOneDbById(object id);
        Task<TEntity> GetOneDbByIdAsync(object id);
        TEntity GetOneDbById(object id1, object id2);
        Task<TEntity> GetOneDbByIdAsync(object id1, object id2);

        void Insert(TEntity entity);
        Task<TEntity> InsertAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(object id);

    }
}
