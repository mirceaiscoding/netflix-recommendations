using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.ExtensionMethods;
using Movie4U.Managers.IManagers;
using Movie4U.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Movie4U.Managers
{
    public class WatcherGenresManager: GenericManager<WatcherGenre, WatcherGenreModel, IWatcherGenresRepository>, IWatcherGenresManager
    {
        public static Expression<Func<WatcherGenre, object>>[] includers;

        static WatcherGenresManager()
        {
            includers = new Expression<Func<WatcherGenre, object>>[]
            {
                wg => wg.genre
            };
        }

        private readonly IGenresManager genresManager;

        /**<summary>
         * Constructor.
         * </summary>*/
        public WatcherGenresManager(IWatcherGenresRepository repo, IGenresManager genresManager): base(repo)
        {
            this.genresManager = genresManager;
        }


        private async Task<bool> FillModelsLists(WatcherGenreModel watcherGenreModel)
        {
            if (watcherGenreModel == null)
                return false;

            watcherGenreModel.genreModel = await genresManager.GetOneByIdAsync(watcherGenreModel.genre_id);
            if (watcherGenreModel.genreModel == null)
            {
                Console.WriteLine(String.Format("WatcherGenresManager.FillModelsList:  genresManager.GetOneById({0}) retrieved a null result.", watcherGenreModel.genre_id));
                return false;
            }

            return true;
        }

        public async Task<List<WatcherGenreModel>> GetAllFromPageAsync(GetAllConfig<WatcherGenre> config = null, WatcherModel watcherModel = null)
        {
            Func<List<WatcherGenreModel>, Task> filler = async watcherGenreModels =>
            {
                foreach (var watcherGenreModel in watcherGenreModels)
                    await FillModelsLists(watcherGenreModel);
            };

            if (config == null)
                config = new GetAllConfig<WatcherGenre>();

            config.includers = includers;

            if (watcherModel == null)
                return await repo.GetAllFromPageAsync(config, null, filler);

            config.extraEntityFilters = new List<Func<IQueryable<WatcherGenre>, IQueryable<WatcherGenre>>>();
            config.extraEntityFilters.Add(source => source.propertyFilter("watcher_name", watcherModel.watcher_name));

            return await repo.GetAllFromPageAsync(config, null, filler);
        }

        public async Task<WatcherGenreModel> GetOneByIdAsync(string watcher_name, int genre_id)
        {
            Func<WatcherGenreModel, Task> filler = async watcherGenreModel =>
                await FillModelsLists(watcherGenreModel);

            var watcherGenreModel = await repo.GetOneByIdAsync(watcher_name, genre_id, filler);
            if(watcherGenreModel != null)
                return watcherGenreModel;

            await Create(new WatcherGenreModelParameter(watcher_name, genre_id, 0));
            return await repo.GetOneByIdAsync(watcher_name, genre_id, filler);
        }

        public async Task Create(WatcherGenreModelParameter watcherGenreModelParameter)
        {
            var newWatcherGenre = new WatcherGenre(watcherGenreModelParameter);

            await repo.InsertAsync(newWatcherGenre);
        }

        public async Task<bool> AddToScore(WatcherGenreModelParameter watcherGenreModelParam)
        {
            var updateWatcherGenre = await repo.GetOneDbByIdAsync(watcherGenreModelParam.watcher_name, watcherGenreModelParam.genre_id);
            if (updateWatcherGenre == null)
                return false;

            updateWatcherGenre.watcherGenreScore += watcherGenreModelParam.watcherGenreScore;

            return await repo.UpdateAsync(updateWatcherGenre);
        }

        public async Task AddToScoreMultiple(string watcher_name, WatcherGenreChangeModel[] changeModels)
        {
            List<WatcherGenre> updatedWatcherGenres = new List<WatcherGenre>();

            foreach (var changeModel in changeModels)
            {
                var watcherGenre = await repo.GetOneDbByIdAsync(watcher_name, changeModel.genre_id);
                if (watcherGenre == null)
                    // skip it
                    continue;

                watcherGenre.watcherGenreScore += changeModel.watcherGenreScore;
                updatedWatcherGenres.Add(watcherGenre);
            }

            await repo.InsertOrUpdateMultipleAsync(updatedWatcherGenres.ToArray());
        }

        public async Task<bool> Update(WatcherGenreModelParameter watcherGenreModelParam)
        {
            var updateWatcherGenre = await repo.GetOneDbByIdAsync(watcherGenreModelParam.watcher_name, watcherGenreModelParam.genre_id);
            if (updateWatcherGenre == null)
                return false;

            updateWatcherGenre.Copy(watcherGenreModelParam);

            return await repo.UpdateAsync(updateWatcherGenre);
        }

    }
}