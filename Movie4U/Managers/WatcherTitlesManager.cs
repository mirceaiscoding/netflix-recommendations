using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Managers.IManagers;
using Movie4U.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers
{
    public class WatcherTitlesManager: IWatcherTitlesManager
    {
        private readonly IWatcherTitlesRepository repo;
        private readonly ITitlesManager titlesManager;

        /**<summary>
         * Constructor.
         * </summary>*/
        public WatcherTitlesManager(IWatcherTitlesRepository repo, ITitlesManager titlesManager)
        {
            this.repo = repo;
            this.titlesManager = titlesManager;
        }


        private async Task<bool> FillModelsLists(WatcherTitleModel watcherTitleModel)
        {
            watcherTitleModel.titleModel = await titlesManager.GetOneByIdAsync(watcherTitleModel.netflix_id);

            return true;
        }

        public async Task<List<WatcherTitleModel>> GetAllFromPageAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1)
        {
            var watcherTitleModels = await repo.GetAllFromPageAsync(orderByFlagsPacked, whereFlagsPacked, pageIndex);

            foreach (var watcherTitleModel in watcherTitleModels)
                await FillModelsLists(watcherTitleModel);

            return watcherTitleModels;
        }

        public async Task<List<WatcherTitleModel>> GetAllByWatcherIdFromPageAsync(string watcher_name, int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1)
        {
            List<Func<WatcherTitle, bool>> extraFilters = new List<Func<WatcherTitle, bool>>();
            extraFilters.Add(wt => wt.watcher_name == watcher_name);

            var watcherTitleModels = await repo.GetAllFromPageAsync(orderByFlagsPacked, whereFlagsPacked, pageIndex, extraFilters);

            foreach (var watcherTitleModel in watcherTitleModels)
                await FillModelsLists(watcherTitleModel);

            return watcherTitleModels;
        }

        public async Task<WatcherTitleModel> GetOneByIdAsync(string watcher_name, string netflix_id)
        {
            var watcherTitleModel = await repo.GetOneByIdAsync(watcher_name, netflix_id);
            await FillModelsLists(watcherTitleModel);

            return watcherTitleModel;
        }

        public async Task Create(WatcherTitleModelParameter watcherTitleModelParam)
        {
            WatcherTitle newWatcherTitle = new WatcherTitle(watcherTitleModelParam);

            await repo.InsertAsync(newWatcherTitle);
        }

        public async Task Update(WatcherTitleModelParameter watcherTitleModelParam)
        {
            WatcherTitle updateWatcherTitle = await repo.GetOneDbByIdAsync(watcherTitleModelParam.watcher_name, watcherTitleModelParam.netflix_id);
            updateWatcherTitle.Copy(watcherTitleModelParam);

            await repo.UpdateAsync(updateWatcherTitle);
        }

        public async Task Delete(string watcher_name, string netflix_id)
        {
            WatcherTitle delWatcherTitle = await repo.GetOneDbByIdAsync(watcher_name, netflix_id);

            if (delWatcherTitle != null)
                await repo.DeleteAsync(delWatcherTitle);
        }

    }
}