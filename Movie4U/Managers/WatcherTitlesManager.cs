using Movie4U.Configurations;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Enums;
using Movie4U.ExtensionMethods;
using Movie4U.Managers.IManagers;
using Movie4U.Repositories.IRepositories;
using Movie4U.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Movie4U.Managers
{
    public class WatcherTitlesManager: GenericManager<WatcherTitle, WatcherTitleModel, IWatcherTitlesRepository>, IWatcherTitlesManager
    {
        public static Expression<Func<WatcherTitle, object>>[] includers;

        static WatcherTitlesManager()
        {
            includers = new Expression<Func<WatcherTitle, object>>[]
            {
                wt => wt.title.titleCountries,
                wt => wt.title.titleGenres
            };
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public WatcherTitlesManager(IWatcherTitlesRepository repo, ITitlesManager titlesManager, IWatcherGenresManager watcherGenresManager) : base(repo) { }


        public async Task<List<WatcherTitleModel>> GetAllFromPageAsync(GetAllConfig<WatcherTitle> config = null, WatcherModel watcherModel = null)
        {
            if (config == null)
                config = new GetAllConfig<WatcherTitle>();

            config.includers = includers;
            config.asSplitQuery = true;

            if (watcherModel == null)    // If there is no watcher specified, we retrieve all the WatcherTitles of all watchers (AdminPolicy only).
                return await repo.GetAllFromPageAsync(config);

            config.extraEntityFilters = new List<Func<IQueryable<WatcherTitle>, IQueryable<WatcherTitle>>>()
            {
                source => source.PropertyFilter("watcher_name", watcherModel.watcher_name)
            };

            if ((config.whereFlagsPacked & (int)WhereEnum.WatcherCountryOnly) == 0  ||  watcherModel.countryId == null)
                return await repo.GetAllFromPageAsync(config);

            List<Func<WatcherTitleModel, bool>> extraModelFilters = new List<Func<WatcherTitleModel, bool>>()
            {
                wt => wt
                .countryModels
                .Where(countryModel => countryModel.id == watcherModel.countryId)
                .Any()
            };
            config.whereFlagsPacked ^= (int)WhereEnum.WatcherCountryOnly;      // We created the extraModelFilter for this flag, so we do not need to evaluate it furher.

            return await repo.GetAllFromPageAsync(config, extraModelFilters);
        }

        public async Task<WatcherTitleModel> GetOneByIdAsync(string watcher_name, int netflix_id)
        {
            var watcherTitleModel = await repo
                .GetOneByIdAsync(
                    GetOneConfigFactory<WatcherTitle, WatcherTitleModel>.New(
                        new object[] { watcher_name, netflix_id},
                        includers,
                        true));

            if(watcherTitleModel != null)
                return watcherTitleModel;

            await Create(watcher_name, new WatcherTitleModelParameter(netflix_id, WatcherTitle.Preferences.Null, DateTime.Now, false, DateTime.Now));

            return await repo
                .GetOneByIdAsync(
                    GetOneConfigFactory<WatcherTitle, WatcherTitleModel>.New(
                        new object[] { watcher_name, netflix_id },
                        includers,
                        true));
        }

        public async Task Create(string watcherName, WatcherTitleModelParameter wtmParam)
        {
            var newWatcherTitle = new WatcherTitle(watcherName, wtmParam);

            await repo.InsertAsync(newWatcherTitle);
        }

        public async Task<bool> Update(string watcherName, WatcherTitleModelParameter wtmParam)
        {
            var updateWatcherTitle = await repo
                .GetOneDbByIdAsync(
                    GetOneConfigFactory<WatcherTitle, WatcherTitleModel>.New(
                        new object[] { watcherName, wtmParam.netflix_id },
                        includers,
                        true));

            if (updateWatcherTitle == null)
                return false;

            updateWatcherTitle.Copy(watcherName, wtmParam);

            return await repo.UpdateAsync(updateWatcherTitle);
        }

        public async Task CreateOrUpdateMultiple(string watcherName, WatcherTitleModelParameter[] wtmParams)
        {
            WatcherTitle[] titles = Array.ConvertAll(wtmParams, wtModelParam => new WatcherTitle(watcherName, wtModelParam));

            await repo.InsertOrUpdateMultipleAsync(titles);
        }

    }
}