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
    public class WatcherTitlesRepository: GenericRepository<WatcherTitle, WatcherTitleModel>, IWatcherTitlesRepository
    {
        /**<summary>
        * Constructor.
        * </summary>>*/
        public WatcherTitlesRepository(Movie4UContext db) : base(db) { }


        public async Task<List<WatcherTitleModel>> GetAllByWatcherIdAsync(string watcher_name)
        {
            return await GetAllDbFilteredQueryableAsync()
                .Result
                .Where(wt => wt.watcher_name == watcher_name)
                .Select(wt => EntitiesModelsFactory<WatcherTitle, WatcherTitleModel>.getModel(wt))
                .ToListAsync();
        }
        
    }
}
