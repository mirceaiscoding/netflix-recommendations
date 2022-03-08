using Movie4U.Entities;
using Movie4U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Repositories
{
    public interface IWatchersRepository
    {
        Task<List<WatcherModel>> GetAllAsync();

        Task<Watcher> GetDbWatcherAsync(string name);

        Task<WatcherModel> GetWatcherAsync (string name);

        Task Create(Watcher watcher);

        Task Update(Watcher watcher);

        Task Delete(Watcher watcher);
    }
}
