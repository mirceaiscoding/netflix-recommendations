using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Repositories.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface IWatcherTitlesManager: IGenericManager<WatcherTitle, WatcherTitleModel, IWatcherTitlesRepository>
    {
        Task<List<WatcherTitleModel>> GetAllFromPageAsync(GetAllConfig<WatcherTitle> config = null, WatcherModel watcherModel = null);

        Task<WatcherTitleModel> GetOneByIdAsync(string watcher_name, int netflix_id);

        Task<bool> Update(WatcherTitleModelParameter watcherTitleModelParam);

        Task Create(WatcherTitleModelParameter watcherTitleModelParam);

        Task CreateOrUpdateMultiple(WatcherTitleModelParameter[] models);
    }
}
