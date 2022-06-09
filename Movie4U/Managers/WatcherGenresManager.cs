using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Managers.IManagers;
using Movie4U.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers
{
    public class WatcherGenresManager: IWatcherGenresManager
    {
        private readonly IWatcherGenresRepository repo;
        private readonly IGenresManager genresManager;

        /**<summary>
         * Constructor.
         * </summary>*/
        public WatcherGenresManager(IWatcherGenresRepository repo, IGenresManager genresManager)
        {
            this.repo = repo;
            this.genresManager = genresManager;
        }


        private async Task<bool> FillModelsLists(WatcherGenreModel watcherGenreModel)
        {
            watcherGenreModel.genreModel = await genresManager.GetOneByIdAsync(watcherGenreModel.genre_id);

            return true;
        }

        public async Task<List<WatcherGenreModel>> GetAllFromPageAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1)
        {
            var watcherGenreModels = await repo.GetAllFromPageAsync(orderByFlagsPacked, whereFlagsPacked, pageIndex);

            foreach (var watcherGenreModel in watcherGenreModels)
                await FillModelsLists(watcherGenreModel);

            return watcherGenreModels;
        }

        public async Task<List<WatcherGenreModel>> GetAllByWatcherIdFromPageAsync(string watcher_name, int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1)
        {
            List<Func<WatcherGenre, bool>> extraFilters = new List<Func<WatcherGenre, bool>>();
            extraFilters.Add(wg => wg.watcher_name == watcher_name);

            var watcherGenreModels = await repo.GetAllFromPageAsync(orderByFlagsPacked, whereFlagsPacked, pageIndex, extraFilters);

            foreach (var watcherGenreModel in watcherGenreModels)
                await FillModelsLists(watcherGenreModel);

            return watcherGenreModels;
        }

        public async Task<WatcherGenreModel> GetOneByIdAsync(string watcher_name, int genre_id)
        {
            var watcherGenreModel = await repo.GetOneByIdAsync(watcher_name, genre_id);
            await FillModelsLists(watcherGenreModel);

            return watcherGenreModel;
        }

        public async Task Create(WatcherGenreModelParameter watcherGenreModelParameter)
        {
            WatcherGenre newWatcherGenre = new WatcherGenre(watcherGenreModelParameter);

            await repo.InsertAsync(newWatcherGenre);
        }

        public async Task AddToScore(WatcherGenreModelParameter watcherGenreModelParam)
        {
            WatcherGenre updateWatcherGenre = await repo.GetOneDbByIdAsync(watcherGenreModelParam.watcher_name, watcherGenreModelParam.genre_id);
            updateWatcherGenre.watcherGenreScore += watcherGenreModelParam.watcherGenreScore;

            await repo.UpdateAsync(updateWatcherGenre);
        }

        public async Task Update(WatcherGenreModelParameter watcherGenreModelParam)
        {
            WatcherGenre updateWatcherGenre = await repo.GetOneDbByIdAsync(watcherGenreModelParam.watcher_name, watcherGenreModelParam.genre_id);
            updateWatcherGenre.Copy(watcherGenreModelParam);

            await repo.UpdateAsync(updateWatcherGenre);
        }

        public async Task Delete(string watcher_name, int genre_id)
        {
            WatcherGenre WatcherGenre = await repo.GetOneDbByIdAsync(watcher_name, genre_id);

            if (WatcherGenre != null)
                await repo.DeleteAsync(WatcherGenre);
        }

    }
}