using Microsoft.EntityFrameworkCore;
using Movie4U.Entities;
using Movie4U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Repositories
{
    public class WatchersRepository: GenericRepository<Watcher>, IWatchersRepository
    {
        /**<summary>
         * The context.
         * </summary>*/
        private readonly Movie4UContext db;

        /**<summary>
         * Constructor.
         * </summary>*/
        public WatchersRepository(Movie4UContext db) : base(db)
        {
            this.db = db;
        }

        public async Task<List<WatcherModel>> GetAllAsync()
        {
            var watchers = await db.Watchers.ToListAsync();

            var watcherModels = new List<WatcherModel> { };
            foreach (var watcher in watchers)
            {
                var watcherModel = new WatcherModel();
                watcherModel.Copy(watcher);

                watcherModels.Add(watcherModel);
            }

            return watcherModels;
        }

        public async Task<WatcherModel> GetOneByNameAsync(string name)
        {
            var watcher = await GetOneDbByIdAsync(name);

            if(watcher != null)
            {
                var watcherModel = new WatcherModel { };
                watcherModel.Copy(watcher);

                return watcherModel;
            }

            return null;
        }

    }
}
