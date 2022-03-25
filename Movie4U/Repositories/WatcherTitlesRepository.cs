using Microsoft.EntityFrameworkCore;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Repositories.IRepositories;
using Movie4U.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Repositories
{
    public class WatcherTitlesRepository: GenericRepository<WatcherTitle, WatcherTitleModel>, IWatcherTitlesRepository
    {
        /**<summary>
        * Constructor.
        * </summary>>*/
        public WatcherTitlesRepository(Movie4UContext db) : base(db) { }


        public async Task<List<WatcherTitleModel>> GetAllAsync()
        {
            var watcherTitles = await db.WatcherTitles.ToListAsync();

            return CastUtility.ToModelsList<WatcherTitle, WatcherTitleModel>(watcherTitles);
        }

        public async Task<List<WatcherTitleModel>> GetAllByWatcherIdAsync(string watcher_name)
        {
            var allDb = await GetAllDbAsync();
            var watcherTitles =
                allDb
                .Where(wt => wt.watcher_name == watcher_name)
                .ToList();

            return CastUtility.ToModelsList<WatcherTitle, WatcherTitleModel>(watcherTitles);
        }

        public async Task<WatcherTitleModel> GetOneByIdAsync(string watcher_name, string netflix_id)
        {
            var watcherTitle = await
                db
                .WatcherTitles
                .FirstOrDefaultAsync(wt => wt.watcher_name == watcher_name && wt.netflix_id == netflix_id);

            if (watcherTitle != null)
                return new WatcherTitleModel(watcherTitle);

            return null;
        }

    }
}
