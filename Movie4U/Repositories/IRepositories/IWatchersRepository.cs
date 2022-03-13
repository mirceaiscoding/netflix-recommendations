using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Repositories.IRepositories
{
    public interface IWatchersRepository : IGenericRepository<Watcher>
    {
        Task<List<WatcherModel>> GetAllAsync();

        Task<WatcherModel> GetOneByIdAsync (string name);
    }
}
