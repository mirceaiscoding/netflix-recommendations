using Movie4U.EntitiesModels;
using Movie4U.EntitiesModels.Models;
using Movie4U.Managers.IManagers;
using Movie4U.Repositories.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers
{
    public class GenericManager<TEntity, TModel, IRepository> : IGenericManager<TEntity, TModel, IRepository>
        where TEntity : EntitiesModelsBase<TEntity, TModel>, new()
        where TModel : EntitiesModelsBase<TEntity, TModel>, new()
        where IRepository : IGenericRepository<TEntity, TModel>
    {
        protected readonly IRepository repo;

        public GenericManager(IRepository repo)
        {
            this.repo = repo;
        }


        public async Task<List<TModel>> GetAllFromPageAsync(GetAllConfig<TEntity> config = null)
        {
            return await repo.GetAllFromPageAsync(config);
        }

        public async Task<TModel> GetOneByIdAsync(object id)
        {
            return await repo.GetOneByIdAsync(id);
        }

        public async Task<TModel> GetOneByIdAsync(object id1, object id2)
        {
            return await repo.GetOneByIdAsync(id1, id2);
        }

        public async Task Create(TModel model)
        {
            if (model == null)
                return;

            var entity = new TEntity();
            entity.Copy(model);

            await repo.InsertAsync(entity);
        }

        public async Task<bool> Update(TModel model)
        {
            if (model == null)
                return false;

            var ids = model.GetId();
            if (ids == null)
                return false;

            TEntity updateEntity;
            switch(ids.count)
            {
                case 1:
                        updateEntity = await repo.GetOneDbByIdAsync(ids.id1);
                    break;
                case 2:
                        updateEntity = await repo.GetOneDbByIdAsync(ids.id1, ids.id2);
                    break;
                default:
                    return false;
            }

            if (updateEntity == null)
                return false;

            updateEntity.Copy(model);

            return await repo.UpdateAsync(updateEntity);
        }

        public async Task<bool> Delete(object id)
        {
            var delEntity = await repo.GetOneDbByIdAsync(id);
            if (delEntity == null)
                return false;

            return await repo.DeleteAsync(delEntity);
        }

        public async Task<bool> Delete(object id1, object id2)
        {
            var delEntity = await repo.GetOneDbByIdAsync(id1, id2);
            if (delEntity == null)
                return false;

            return await repo.DeleteAsync(delEntity);
        }
    }
}
