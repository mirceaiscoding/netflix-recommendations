﻿using Movie4U.EntitiesModels.Entities;
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
        private readonly IWatcherGenresManager watcherGenresManager;

        /**<summary>
         * Constructor.
         * </summary>*/
        public WatcherTitlesManager(IWatcherTitlesRepository repo, ITitlesManager titlesManager, IWatcherGenresManager watcherGenresManager)
        {
            this.repo = repo;
            this.titlesManager = titlesManager;
            this.watcherGenresManager = watcherGenresManager;
        }


        private async Task<bool> FillModelsLists(WatcherTitleModel watcherTitleModel)
        {
            if (watcherTitleModel == null)
                return false;

            var titleModel = await titlesManager.GetOneByIdAsync(watcherTitleModel.netflix_id);

            watcherTitleModel.synopsis = titleModel.synopsis;
            watcherTitleModel.year = titleModel.year;
            watcherTitleModel.poster = titleModel.poster;
            watcherTitleModel.rating = titleModel.rating;
            watcherTitleModel.countryModels = titleModel.countryModels;

            var watcherGenreModels = new List<WatcherGenreModel>();
            foreach(var genreModel in titleModel.genreModels)
            {
                var watcherGenreModel = await watcherGenresManager.GetOneByIdAsync(watcherTitleModel.watcher_name, genreModel.genre_id);
                watcherGenreModels.Add(watcherGenreModel);
            }
            watcherTitleModel.watcherGenreModels = watcherGenreModels;

            return true;
        }

        public async Task<List<WatcherTitleModel>> GetAllFromPageAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1)
        {
            Func<List<WatcherTitleModel>, Task> filler = async watcherTitleModels =>
            {
                foreach (var watcherTitleModel in watcherTitleModels)
                    await FillModelsLists(watcherTitleModel);
            };

            return await repo.GetAllFromPageAsync(orderByFlagsPacked, whereFlagsPacked, pageIndex, null, filler);
        }

        public async Task<List<WatcherTitleModel>> GetAllByWatcherIdFromPageAsync(string watcher_name, int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1)
        {
            List<Func<WatcherTitle, bool>> extraFilters = new List<Func<WatcherTitle, bool>>();
            extraFilters.Add(wt => wt.watcher_name == watcher_name);

            Func<List<WatcherTitleModel>, Task> filler = async watcherTitleModels =>
            {
                foreach (var watcherTitleModel in watcherTitleModels)
                    await FillModelsLists(watcherTitleModel);
            };

            return await repo.GetAllFromPageAsync(orderByFlagsPacked, whereFlagsPacked, pageIndex, extraFilters, filler);
        }

        public async Task<WatcherTitleModel> GetOneByIdAsync(string watcher_name, string netflix_id)
        {
            Func<WatcherTitleModel, Task> filler = async watcherTitleModel =>
                await FillModelsLists(watcherTitleModel);

            return await repo.GetOneByIdAsync(watcher_name, netflix_id, filler);
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