using Movie4U.Configurations;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.ExtensionMethods;
using Movie4U.Managers.IManagers;
using Movie4U.Repositories.IRepositories;
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
            if (config == null)
                config = new GetAllConfig<TitleGenre>();

            config.extraEntityFilters = new List<Func<IQueryable<TitleGenre>, IQueryable<TitleGenre>>>();
            config.extraEntityFilters.Add(source => source.propertyFilter("netflix_id", netflixId));

            return await repo.GetAllFromPageAsync(config);
        }

        public async Task<List<GenreModel>> GetAllGenresByNetflixIdFromPageAsync(int netflixId, GetAllConfig<TitleGenre> config = null)
        {
            var titleGenres = await GetAllByNetflixIdFromPageAsync(netflixId, config);

            var genres = new List<GenreModel>();
            foreach(var titleGenre in titleGenres)
            {
                var genre = await genresManager.GetOneByIdAsync(titleGenre.genre_id);
                genres.Add(genre);
            }

            return genres;
        }

        public async Task CreateOrUpdateMultiple(TitleGenreModel[] models)
        {
            TitleGenre[] titleGenres = Array.ConvertAll(models, x => new TitleGenre(x));

            await repo.InsertOrUpdateMultipleAsync(titleGenres);
        }

    }
}
