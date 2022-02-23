using Movie4U.Entities;
using Movie4U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Repositories
{
    public class WatchersRepository: IWatchersRepository
    {
        // the context
        private readonly Movie4UContext db;

        // the constructor
        public WatchersRepository(Movie4UContext db)
        {
            this.db = db;
        }

        public List<WatcherModel> GetAll()
        {
            var watchers = db.Watchers.ToList();

            var watcherModels = new List<WatcherModel> { };
            foreach (var watcher in watchers)
            {
                var watcherModel = new WatcherModel
                {
                    watcher_name = watcher.watcher_name,
                    register_date = watcher.register_date
                };
                watcherModels.Add(watcherModel);
            }

            return watcherModels;
        }

        public Watcher GetDbWatcher(string name)
        {
            var watcher = db.Watchers.FirstOrDefault(watcher => watcher.watcher_name == name);

            return watcher;
        }

        public WatcherModel GetWatcher(string name)
        {
            var watcher = GetDbWatcher(name);

            if(watcher != null)
            {
                var watcherModel = new WatcherModel { };
                watcherModel.copy(watcher);

                return watcherModel;
            }

            return null;
        }

        public async Task Create(Watcher watcher)
        {
            await db.Watchers.AddAsync(watcher);

            await db.SaveChangesAsync();
        }

        public async Task Update(Watcher watcher)
        {
            db.Watchers.Update(watcher);

            await db.SaveChangesAsync();
        }

        public async Task Delete(Watcher watcher)
        {
            db.Watchers.Remove(watcher);

            await db.SaveChangesAsync();
        }
    }
}
