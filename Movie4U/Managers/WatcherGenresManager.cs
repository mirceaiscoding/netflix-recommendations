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
            if (watcherGenreModel == null)
                return false;

            watcherGenreModel.genreModel = await genresManager.GetOneByIdAsync(watcherGenreModel.genre_id);

            return true;
        }

        public async Task<List<WatcherGenreModel>> GetAllFromPageAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1)
        {
            Func<List<WatcherGenreModel>, Task> filler = async watcherGenreModels =>
            {
                foreach (var watcherGenreModel in watcherGenreModels)
                    await FillModelsLists(watcherGenreModel);
            };

            return await repo.GetAllFromPageAsync(orderByFlagsPacked, whereFlagsPacked, pageIndex, null, filler);
        }

        public async Task<List<WatcherGenreModel>> GetAllByWatcherIdFromPageAsync(string watcher_name, int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1)
        {
            List<Func<WatcherGenre, bool>> extraFilters = new List<Func<WatcherGenre, bool>>();
            extraFilters.Add(wg => wg.watcher_name == watcher_name);

            Func<List<WatcherGenreModel>, Task> filler = async watcherGenreModels =>
            {
                foreach (var watcherGenreModel in watcherGenreModels)
                    await FillModelsLists(watcherGenreModel);
            };

            return await repo.GetAllFromPageAsync(orderByFlagsPacked, whereFlagsPacked, pageIndex, extraFilters, filler);
        }

        public async Task<WatcherGenreModel> GetOneByIdAsync(string watcher_name, int genre_id)
        {
            Func<WatcherGenreModel, Task> filler = async watcherGenreModel =>
                await FillModelsLists(watcherGenreModel);

            return await repo.GetOneByIdAsync(watcher_name, genre_id, filler);
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