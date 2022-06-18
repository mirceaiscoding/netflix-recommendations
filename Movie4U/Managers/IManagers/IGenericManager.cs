using Movie4U.Configurations;
using Movie4U.EntitiesModels;
using Movie4U.Repositories.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface IGenericManager<TEntity, TModel, IRepository>
        where TEntity: EntitiesModelsBase<TEntity, TModel>, IEntity<TEntity>, new()
        where TModel: EntitiesModelsBase<TEntity, TModel>, IModel<TModel>, new()
        where IRepository: IGenericRepository<TEntity, TModel>
    {
        Task<List<TModel>> GetAllFromPageAsync(GetAllConfig<TEntity> config = null);

        Task<TModel> GetOneByIdAsync(params object[] ids);

        Task<bool> Update(TModel model);

        Task Create(TModel model);

        Task<bool> Delete(params object[] ids);

    }
}
