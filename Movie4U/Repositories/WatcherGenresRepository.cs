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
    public class WatcherGenresRepository : GenericRepository<WatcherGenre>, IWatcherGenresRepository
    {

        /**<summary>
        * Constructor.
        * </summary>>*/
        public WatcherGenresRepository(Movie4UContext db) : base(db) { }


        public async Task<List<WatcherGenreModel>> GetAllAsync()
        {
            var watcherGenres = await db.WatcherGenres.ToListAsync();

            return CastUtility.ToModelsList<WatcherGenre, WatcherGenreModel>(watcherGenres);
        }

        public async Task<List<WatcherGenreModel>> GetAllByWatcherIdAsync(string watcher_name)
        {
            var allDb = await GetAllDbAsync();
            var watcherGenres =
                allDb
                .Where(wg => wg.watcher_name == watcher_name)
                .ToList();

            return CastUtility.ToModelsList<WatcherGenre, WatcherGenreModel>(watcherGenres);
        }

        public async Task<WatcherGenreModel> GetOneByIdAsync(string watcher_name, int genre_id)
        {
            var watcherGenre = await
                db
                .WatcherGenres
                .FirstOrDefaultAsync(wg => wg.watcher_name == watcher_name && wg.genre_id== genre_id);

            if (watcherGenre != null)
                return new WatcherGenreModel(watcherGenre);

            return null;
        }
    }
}