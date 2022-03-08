using Movie4U.Entities;
using Movie4U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Repositories
{
    public interface IWatchersRepository : IGenericRepository<Watcher>
    {
        Task<List<WatcherModel>> GetAllWatcherModelsAsync();

        Task<WatcherModel> GetWatcherModelByNameAsync (string name);
    }
}
