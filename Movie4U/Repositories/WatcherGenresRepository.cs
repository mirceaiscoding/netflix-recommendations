using Microsoft.EntityFrameworkCore;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Repositories.IRepositories;
using Movie4U.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Repositories
{
    public class WatcherGenresRepository : GenericRepository<WatcherGenre, WatcherGenreModel>, IWatcherGenresRepository
    {

        /**<summary>
        * Constructor.
        * </summary>>*/
        public WatcherGenresRepository(Movie4UContext db) : base(db) { }


        public async Task<List<WatcherGenreModel>> GetAllByWatcherIdAsync(string watcher_name)
        {
            return await GetAllDbFilteredQueryableAsync()
                .Result
                .Where(wg => wg.watcher_name == watcher_name)
                .Select(wg => EntitiesModelsFactory<WatcherGenre, WatcherGenreModel>.getModel(wg))
                .ToListAsync();
        }

    }
}