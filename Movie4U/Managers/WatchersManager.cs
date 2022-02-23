using Movie4U.Entities;
using Movie4U.Models;
using Movie4U.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Managers
{
    public class WatchersManager: IWatchersManager
    {
        private readonly IWatchersRepository repo;
        
        public WatchersManager(IWatchersRepository repo)
        {
            this.repo = repo;
        }

        public List<WatcherModel> GetAll()
        {
            return repo.GetAll();
        }

        public WatcherModel GetWatcher(string name)
        {
            return repo.GetWatcher(name);
        }

        public async Task Create(string watcherName, string UserId)
        {
            Watcher newWatcher = new Watcher
            {
                watcher_name = watcherName,
                register_date = DateTime.Now,
                userId = UserId
            };

            await repo.Create(newWatcher);
        }



        public async Task Delete(string name)
        {
            Watcher watcher = repo.GetDbWatcher(name);

            if (watcher != null)
                await repo.Delete(watcher);
        }
    }
}
