using Movie4U.Configurations;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Repositories.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface IWatcherGenresManager: IGenericManager<WatcherGenre, WatcherGenreModel, IWatcherGenresRepository>
    {
        Task<List<WatcherGenreModel>> GetAllFromPageAsync(GetAllConfig<WatcherGenre> config = null, WatcherModel watcherModel = null);

        Task<WatcherGenreModel> GetOneByIdAsync(string watcher_name, int genre_id);

        Task AddToScoreMultiple(string watcher_name, WatcherGenreChangeModel[] changeModels);

        Task<bool> AddToScore(string watcherName, WatcherGenreModelParameter wgmParam);

        Task<bool> Update(string watcherName, WatcherGenreModelParameter wgmParam);

        Task Create(string watcherName, WatcherGenreModelParameter wgmParam);
    }
}
