using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Enums;
using Movie4U.Managers.IManagers;
using Movie4U.Repositories.IRepositories;
using Movie4U.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if(titleModel == null)
            {
                Console.WriteLine(String.Format("WatcherTitlesManager.FillModelsLists:  titlesManager.GetOneByIdAsync({0}, {1}) method retrieved a null result.", watcherTitleModel.netflix_id));
                return false;
            }

            watcherTitleModel.synopsis = titleModel.synopsis;
            watcherTitleModel.year = titleModel.year;
            watcherTitleModel.poster = titleModel.poster;
            watcherTitleModel.rating = titleModel.rating;
            watcherTitleModel.countryModels = titleModel.countryModels;
            watcherTitleModel.title = titleModel.title;

            var watcherGenreModels = new List<WatcherGenreModel>();
            foreach(var genreModel in titleModel.genreModels)
            {
                var watcherGenreModel = await watcherGenresManager.GetOneByIdAsync(watcherTitleModel.watcher_name, genreModel.genre_id);
                if(watcherGenreModel == null)
                {
                    Console.WriteLine(String.Format("WatcherTitlesManager.FillModelsLists:  watcherGenresManager.GetOneByIdAsync({0}, {1}) method retrieved a null result.", watcherTitleModel.watcher_name, genreModel.genre_id));
                    continue;
                }

                watcherGenreModels.Add(watcherGenreModel);
            }

            watcherTitleModel.watcherGenreModels = watcherGenreModels;
            return true;
        }

        public async Task<List<WatcherTitleModel>> GetAllFromPageAsync(GetAllConfig<WatcherTitle> config = null, WatcherModel watcherModel = null)
        {
            Func<List<WatcherTitleModel>, Task> filler = async watcherTitleModels =>
            {
                foreach (var watcherTitleModel in watcherTitleModels)
                    await FillModelsLists(watcherTitleModel);
            };

            if(watcherModel == null)    // If there is no watcher specified, we retrieve all the WatcherTitles of all watchers (AdminPolicy only).
                return await repo.GetAllFromPageAsync(config, null, filler);

            if (config == null)
                config = new GetAllConfig<WatcherTitle>();

            config.extraEntityFilters = new List<Func<IQueryable<WatcherTitle>, IQueryable<WatcherTitle>>>();
            config.extraEntityFilters.Add(source => ExpressionsUtility.propertyFilter(source, "watcher_name", watcherModel.watcher_name));

            if ((config.whereFlagsPacked & (int)WhereEnum.WatcherCountryOnly) == 0  ||  watcherModel.countryId == null)
                return await repo.GetAllFromPageAsync(config, null, filler);

            List<Func<WatcherTitleModel, bool>> extraModelFilters = new List<Func<WatcherTitleModel, bool>>();
            extraModelFilters.Add(wt =>
                wt
                .countryModels
                .Where(cm =>cm.id == watcherModel.countryId)
                .Count() > 0 );
            config.whereFlagsPacked ^= (int)WhereEnum.WatcherCountryOnly;      // We created the extraModelFilter for this flag, so we do not need to evaluate it furher.

            return await repo.GetAllFromPageAsync(config, extraModelFilters, filler);
        }

        public async Task<WatcherTitleModel> GetOneByIdAsync(string watcher_name, int netflix_id)
        {
            Func<WatcherTitleModel, Task> filler = async watcherTitleModel =>
                await FillModelsLists(watcherTitleModel);

            var watcherTitleModel = await repo.GetOneByIdAsync(watcher_name, netflix_id, filler);
            if(watcherTitleModel != null)
                return watcherTitleModel;

            await Create(new WatcherTitleModelParameter(watcher_name, netflix_id, WatcherTitle.Preferences.Null, DateTime.Now, false, DateTime.Now));
            return await repo.GetOneByIdAsync(watcher_name, netflix_id, filler);
        }

        public async Task Create(WatcherTitleModelParameter watcherTitleModelParam)
        {
            var newWatcherTitle = new WatcherTitle(watcherTitleModelParam);

            await repo.InsertAsync(newWatcherTitle);
        }

        public async Task<bool> Update(WatcherTitleModelParameter watcherTitleModelParam)
        {
            var updateWatcherTitle = await repo.GetOneDbByIdAsync(watcherTitleModelParam.watcher_name, watcherTitleModelParam.netflix_id);
            if (updateWatcherTitle == null)
                return false;

            updateWatcherTitle.Copy(watcherTitleModelParam);

            return await repo.UpdateAsync(updateWatcherTitle);
        }

        public async Task CreateOrUpdateMultiple(WatcherTitleModelParameter[] models)
        {
            WatcherTitle[] titles = Array.ConvertAll(models, x => new WatcherTitle(x));

            await repo.InsertOrUpdateMultipleAsync(titles);
        }

        public async Task<bool> Delete(string watcher_name, int netflix_id)
        {
            var delWatcherTitle = await repo.GetOneDbByIdAsync(watcher_name, netflix_id);
            if (delWatcherTitle == null)
                return false;

            return await repo.DeleteAsync(delWatcherTitle);
        }

    }
}