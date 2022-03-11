using Microsoft.EntityFrameworkCore;
using Movie4U.Entities;
using Movie4U.Models;
using Movie4U.Repositories.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Repositories
{
    public class WatchersRepository: GenericRepository<Watcher>, IWatchersRepository
    {
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

        public async Task<WatcherModel> GetOneByIdAsync(string name)
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
