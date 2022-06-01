using Movie4U.EntitiesModels.Models;
using Movie4U.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface IWatcherGenresManager
    {
        Task<List<WatcherGenreModel>> GetAllAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageNumber = 1);

        Task<List<WatcherGenreModel>> GetAllByWatcherIdAsync(string watcher_name);

        Task<WatcherGenreModel> GetOneByIdAsync(string watcher_name, int genre_id);

        Task Update(WatcherGenreModelParameter watcherGenreModelParam);

        Task Create(WatcherGenreModelParameter watcherGenreModelParam);

        Task Delete(string watcher_name, int genre_id);
    }
}
