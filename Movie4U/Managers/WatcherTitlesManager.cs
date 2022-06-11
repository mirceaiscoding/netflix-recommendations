﻿using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Enums;
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
        private readonly ICountriesManager countriesManager;

        /**<summary>
         * Constructor.
         * </summary>*/
        public WatcherTitlesManager(IWatcherTitlesRepository repo, ITitlesManager titlesManager, IWatcherGenresManager watcherGenresManager, ICountriesManager countriesManager)
        {
            this.repo = repo;
            this.titlesManager = titlesManager;
            this.watcherGenresManager = watcherGenresManager;
            this.countriesManager = countriesManager;
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

        public async Task<List<WatcherTitleModel>> GetAllFromPageAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1, WatcherModel watcherModel = null)
        {
            Func<List<WatcherTitleModel>, Task> filler = async watcherTitleModels =>
            {
                foreach (var watcherTitleModel in watcherTitleModels)
                    await FillModelsLists(watcherTitleModel);
            };

            if(watcherModel == null)    // If there is no watcher specified, we retrieve all the WatcherTitles of all watchers (AdminPolicy only).
                return await repo.GetAllFromPageAsync(orderByFlagsPacked, whereFlagsPacked, pageIndex, null, null, filler);

            List<Func<WatcherTitle, bool>> extraEntityFilters = new List<Func<WatcherTitle, bool>>();
            extraEntityFilters.Add(wt => wt.watcher_name == watcherModel.watcher_name);

            if ((whereFlagsPacked & (int)WhereEnum.WatcherCountryOnly) == 0  ||  watcherModel.coutryId == null)
                return await repo.GetAllFromPageAsync(orderByFlagsPacked, whereFlagsPacked, pageIndex, extraEntityFilters, null, filler);

            var watcherCountry = await countriesManager.GetOneByIdAsync((int)watcherModel.coutryId);
            if(watcherCountry == null)
                return await repo.GetAllFromPageAsync(orderByFlagsPacked, whereFlagsPacked, pageIndex, extraEntityFilters, null, filler);

            List<Func<WatcherTitleModel, bool>> extraModelFilters = new List<Func<WatcherTitleModel, bool>>();
            extraModelFilters.Add(wt => wt.countryModels.Contains(watcherCountry));
            whereFlagsPacked ^= (int)WhereEnum.WatcherCountryOnly;      // We created the extraModelFilter for this flag, so we do not need to evaluate it furher.

            return await repo.GetAllFromPageAsync(orderByFlagsPacked, whereFlagsPacked, pageIndex, extraEntityFilters, extraModelFilters, filler);
        }

        public async Task<WatcherTitleModel> GetOneByIdAsync(string watcher_name, int netflix_id)
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

        public async Task Delete(string watcher_name, int netflix_id)
        {
            WatcherTitle delWatcherTitle = await repo.GetOneDbByIdAsync(watcher_name, netflix_id);

            if (delWatcherTitle != null)
                await repo.DeleteAsync(delWatcherTitle);
        }

    }
}