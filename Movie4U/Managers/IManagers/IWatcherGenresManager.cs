using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface IWatcherGenresManager
    {
        Task<List<WatcherGenreModel>> GetAllFromPageAsync(GetAllConfig<WatcherGenre> config = null, WatcherModel watcherModel = null);

        Task<WatcherGenreModel> GetOneByIdAsync(string watcher_name, int genre_id);

        Task AddToScoreMultiple(string watcher_name, WatcherGenreChangeModel[] changeModels);

        Task<bool> AddToScore(WatcherGenreModelParameter watcherGenreModelParam);

        Task<bool> Update(WatcherGenreModelParameter watcherGenreModelParam);

        Task Create(WatcherGenreModelParameter watcherGenreModelParam);

        Task<bool> Delete(string watcher_name, int genre_id);
    }
}
