using Movie4U.EntitiesModels.Models;
using Movie4U.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface IWatcherGenresManager
    {
        Task<List<WatcherGenreModel>> GetAllFromPageAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1, WatcherModel watcherModel = null);

        Task<WatcherGenreModel> GetOneByIdAsync(string watcher_name, int genre_id);

        Task<bool> AddToScore(WatcherGenreModelParameter watcherGenreModelParam);

        Task<bool> Update(WatcherGenreModelParameter watcherGenreModelParam);

        Task Create(WatcherGenreModelParameter watcherGenreModelParam);

        Task<bool> Delete(string watcher_name, int genre_id);
    }
}
