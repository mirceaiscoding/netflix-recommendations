using Movie4U.EntitiesModels.Models;
using Movie4U.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface IWatcherTitlesManager
    {
        Task<List<WatcherTitleModel>> GetAllFromPageAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1, WatcherModel watcherModel = null);

        Task<WatcherTitleModel> GetOneByIdAsync(string watcher_name, string netflix_id);

        Task Update(WatcherTitleModelParameter watcherTitleModelParam);

        Task Create(WatcherTitleModelParameter watcherTitleModelParam);

        Task Delete(string watcher_name, string netflix_id);
    }
}
