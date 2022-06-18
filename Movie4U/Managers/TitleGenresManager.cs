using Movie4U.Configurations;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Managers.IManagers;
using Movie4U.Repositories.IRepositories;
using Movie4U.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Managers
{
    public class TitleGenresManager: GenericManager<TitleGenre, TitleGenreModel, ITitleGenresRepository>, ITitleGenresManager
    {
        private readonly IGenresManager genresManager;

        /**<summary>
         * Constructor.
         * </summary>*/
        public TitleGenresManager(ITitleGenresRepository repo, IGenresManager genresManager): base(repo)
        {
            this.genresManager = genresManager;
        }


        public async Task<List<TitleGenreModel>> GetAllByNetflixIdFromPageAsync(int netflixId, GetAllConfig<TitleGenre> config = null)
        {
            return await repo.GetAllFromPageAsync(
                NetflixIdDependentsUtility<TitleGenre>
                .AddNetflixIdExtraEntityFilter(
                    netflixId,
                    config));
        }

        public async Task<List<GenreModel>> GetAllGenresByNetflixIdFromPageAsync(int netflixId, GetAllConfig<TitleGenre> config = null)
        {
            var tasks =
                (await GetAllByNetflixIdFromPageAsync(netflixId, config))
                .Select(
                    async tgm => new GenreModel(
                        await genresManager.GetOneByIdAsync(tgm.genre_id)));

            return (await Task.WhenAll(tasks))
                .ToList();
        }

        public async Task CreateOrUpdateMultiple(TitleGenreModel[] models)
        {
            TitleGenre[] titleGenres = Array.ConvertAll(models, x => new TitleGenre(x));

            await repo.InsertOrUpdateMultipleAsync(titleGenres);
        }

    }
}
