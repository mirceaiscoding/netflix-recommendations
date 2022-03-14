using Movie4U.EntitiesModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface IWatcherGenresManager
    {
        Task<List<WatcherGenreModel>> GetAllAsync();

        Task<List<WatcherGenreModel>> GetAllByWatcherIdAsync(string watcher_name);

        Task<WatcherGenreModel> GetOneByIdAsync(string watcher_name, int genre_id);

        Task Update(WatcherGenreModelParameter watcherGenreModelParam);

        Task Create(WatcherGenreModelParameter watcherGenreModelParam);

        Task Delete(string watcher_name, int genre_id);
    }
}
