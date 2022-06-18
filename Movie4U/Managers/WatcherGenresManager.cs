using Movie4U.Configurations;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
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


        public async Task<List<WatcherGenreModel>> GetAllFromPageAsync(GetAllConfig<WatcherGenre> config = null, WatcherModel watcherModel = null)
        {
            if (config == null)
                config = new GetAllConfig<WatcherGenre>();

            config.includers = includers;

            if (watcherModel == null)
                return await repo.GetAllFromPageAsync(config);

            config.extraEntityFilters = new List<Func<IQueryable<WatcherGenre>, IQueryable<WatcherGenre>>>()
            {
                source => source.PropertyFilter("watcher_name", watcherModel.watcher_name)
            };

            return await repo.GetAllFromPageAsync(config);
        }

        public async Task<WatcherGenreModel> GetOneByIdAsync(string watcher_name, int genre_id)
        {
            var watcherGenreModel = await repo
                .GetOneByIdAsync(
                    GetOneConfigFactory<WatcherGenre, WatcherGenreModel>.New(
                        new object[] { watcher_name, genre_id },
                        includers,
                        false));

            if(watcherGenreModel != null)
                return watcherGenreModel;

            await Create(watcher_name, new WatcherGenreModelParameter(genre_id, 0));

            return await repo
                .GetOneByIdAsync(
                    GetOneConfigFactory<WatcherGenre, WatcherGenreModel>.New(
                        new object[] { watcher_name, genre_id },
                        includers,
                        false));
        }

        public async Task Create(string watcherName, WatcherGenreModelParameter wgmParam)
        {
            var newWatcherGenre = new WatcherGenre(watcherName, wgmParam);

            await repo.InsertAsync(newWatcherGenre);
        }

        public async Task<bool> AddToScore(string watcherName, WatcherGenreModelParameter wgmParam)
        {
            var updateWatcherGenre = await repo
                .GetOneDbByIdAsync(
                    GetOneConfigFactory<WatcherGenre, WatcherGenreModel>.New(
                            new object[] { watcherName, wgmParam.genre_id },
                            includers,
                            false));

            if (updateWatcherGenre == null)
                return false;

            updateWatcherGenre.watcherGenreScore += wgmParam.watcherGenreScore;

            return await repo.UpdateAsync(updateWatcherGenre);
        }

        public async Task AddToScoreMultiple(string watcher_name, WatcherGenreChangeModel[] changeModels)
        {
            List<WatcherGenre> updatedWatcherGenres = new List<WatcherGenre>();

            foreach (var changeModel in changeModels)
            {
                var watcherGenre = await repo
                    .GetOneDbByIdAsync(
                        GetOneConfigFactory<WatcherGenre, WatcherGenreModel>.New(
                                new object[] { watcher_name, changeModel.genre_id },
                                includers,
                                false));

                if (watcherGenre == null)
                    continue;

                watcherGenre.watcherGenreScore += changeModel.watcherGenreScore;
                updatedWatcherGenres.Add(watcherGenre);
            }

            await repo.InsertOrUpdateMultipleAsync(updatedWatcherGenres.ToArray());
        }

        public async Task<bool> Update(string watcherName, WatcherGenreModelParameter wgmParam)
        {
            var updateWatcherGenre = await repo
                .GetOneDbByIdAsync(
                    GetOneConfigFactory<WatcherGenre, WatcherGenreModel>.New(
                            new object[] { watcherName, wgmParam.genre_id },
                            includers,
                            false));

            if (updateWatcherGenre == null)
                return false;

            updateWatcherGenre.Copy(watcherName, wgmParam);

            return await repo.UpdateAsync(updateWatcherGenre);
        }

    }
}