using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Enums;
using Movie4U.Managers.IManagers;
using Movie4U.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers
{
    public class TitleGenresManager: ITitleGenresManager
    {
        private readonly ITitleGenresRepository repo;
        private readonly IGenresManager genresManager;

        /**<summary>
         * Constructor.
         * </summary>*/
        public TitleGenresManager(ITitleGenresRepository repo, IGenresManager genresManager)
        {
            this.repo = repo;
            this.genresManager = genresManager;
        }


        public async Task<List<TitleGenreModel>> GetAllFromPageAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1)
        {
            return await repo.GetAllFromPageAsync();
        }

        public async Task<List<TitleGenreModel>> GetAllByNetflixIdFromPageAsync(string netflixId, int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1)
        {
            List<Func<TitleGenre, bool>> extraFilters = new List<Func<TitleGenre, bool>>();
            extraFilters.Add(tg => tg.netflix_id == netflixId);

            return await repo.GetAllFromPageAsync(orderByFlagsPacked, whereFlagsPacked, pageIndex, extraFilters);
        }

        public async Task<List<GenreModel>> GetAllGenresByNetflixIdFromPageAsync(string netflixId, int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1)
        {
            var titleGenres = await GetAllByNetflixIdFromPageAsync(netflixId);

            var genres = new List<GenreModel>();
            foreach(var titleGenre in titleGenres)
            {
                var genre = await genresManager.GetOneByIdAsync(titleGenre.genre_id);
                genres.Add(genre);
            }

            return genres;
        }

        public async Task<TitleGenreModel> GetOneByIdAsync(int genre_id, string netflix_id)
        {
            return await repo.GetOneByIdAsync(genre_id, netflix_id);
        }

        public async Task Create(TitleGenreModel titleGenreModel)
        {
            TitleGenre newTitleGenre = new TitleGenre(titleGenreModel);

            await repo.InsertAsync(newTitleGenre);
        }

        public async Task Update(TitleGenreModel titleGenreModel)
        {
            TitleGenre updateTitleGenre = await repo.GetOneDbByIdAsync(titleGenreModel.genre_id, titleGenreModel.netflix_id);
            updateTitleGenre.Copy(titleGenreModel);

            await repo.UpdateAsync(updateTitleGenre);
        }

        public async Task Delete(int genre_id, string netflix_id)
        {
            TitleGenre delTitleGenre = await repo.GetOneDbByIdAsync(genre_id, netflix_id);

            if (delTitleGenre != null)
                await repo.DeleteAsync(delTitleGenre);
        }

    }
}
