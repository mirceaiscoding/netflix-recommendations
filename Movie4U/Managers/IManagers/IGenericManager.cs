using Movie4U.EntitiesModels;
using Movie4U.EntitiesModels.Models;
using Movie4U.Repositories.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface IGenericManager<TEntity, TModel, IRepository>
        where TEntity: EntitiesModelsBase<TEntity, TModel>, new()
        where TModel: EntitiesModelsBase<TEntity, TModel>, new()
        where IRepository: IGenericRepository<TEntity, TModel>
    {
        Task<List<TModel>> GetAllFromPageAsync(GetAllConfig<TEntity> config = null);

        Task<TModel> GetOneByIdAsync(object id);

        Task<TModel> GetOneByIdAsync(object id1, object id2);

        Task<bool> Update(TModel model);

        Task Create(TModel model);

        Task<bool> Delete(object id);

        Task<bool> Delete(object id1, object id2);

    }
}
