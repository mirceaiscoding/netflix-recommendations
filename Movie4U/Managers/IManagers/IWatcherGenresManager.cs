using Movie4U.EntitiesModels.Models;
using Movie4U.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface IWatcherGenresManager
    {
        Task<List<WatcherGenreModel>> GetAllFromPageAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1);

        Task<List<WatcherGenreModel>> GetAllByWatcherIdFromPageAsync(string watcher_name, int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1);

        Task<WatcherGenreModel> GetOneByIdAsync(string watcher_name, int genre_id);

        Task AddToScore(WatcherGenreModelParameter watcherGenreModelParam);

        Task Update(WatcherGenreModelParameter watcherGenreModelParam);

        Task Create(WatcherGenreModelParameter watcherGenreModelParam);

        Task Delete(string watcher_name, int genre_id);
    }
}
