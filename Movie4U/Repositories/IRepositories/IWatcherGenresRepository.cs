using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Repositories.IRepositories
{
    public interface IWatcherGenresRepository: IGenericRepository<WatcherGenre>
    {
        Task<List<WatcherGenreModel>> GetAllAsync();

        Task<List<WatcherGenreModel>> GetAllByWatcherIdAsync(string watcher_name);

        Task<WatcherGenreModel> GetOneByIdAsync(string watcher_name, int genre_id);
    }
}
