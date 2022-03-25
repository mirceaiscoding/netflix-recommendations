using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Repositories.IRepositories
{
    public interface IWatcherTitlesRepository: IGenericRepository<WatcherTitle, WatcherTitleModel>
    {
        Task<List<WatcherTitleModel>> GetAllAsync();

        Task<List<WatcherTitleModel>> GetAllByWatcherIdAsync(string watcher_name);

        Task<WatcherTitleModel> GetOneByIdAsync(string watcher_name, string netflix_id);
    }
}
