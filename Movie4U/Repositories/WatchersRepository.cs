using Microsoft.EntityFrameworkCore;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Repositories.IRepositories;
using Movie4U.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Repositories
{
    public class WatchersRepository: GenericRepository<Watcher>, IWatchersRepository
    {
        /**<summary>
         * Constructor.
         * </summary>*/
        public WatchersRepository(Movie4UContext db) : base(db) { }


        public async Task<List<WatcherModel>> GetAllAsync()
        {
            var watchers = await db.Watchers.ToListAsync();

            return CastUtility.ToModelsList<Watcher,WatcherModel>(watchers);
        }

        public async Task<WatcherModel> GetOneByIdAsync(string name)
        {
            var watcher = await GetOneDbByIdAsync(name);

            if(watcher != null)
                return new WatcherModel(watcher);

            return null;
        }

    }
}
