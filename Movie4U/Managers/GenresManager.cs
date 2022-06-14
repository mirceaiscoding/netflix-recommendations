using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.EntitiesModels.Models.uNoGS;
using Movie4U.Managers.IManagers;
using Movie4U.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers
{
    public class GenresManager : IGenresManager
    {
        private readonly IGenresRepository repo;

        /**<summary>
         * Constructor.
         * </summary>*/
        public GenresManager(IGenresRepository repo)
        {
            this.repo = repo;
        }


        public async Task<List<GenreModel>> GetAllFromPageAsync(GetAllConfig<Genre> config = null)
        {
            return await repo.GetAllFromPageAsync(config);
        }

        public async Task<GenreModel> GetOneByIdAsync(int genre_id)
        {
            return await repo.GetOneByIdAsync(genre_id);
        }

        public async Task Create(GenreModel genreModel)
        {
            var newGenre = new Genre(genreModel);

            await repo.InsertAsync(newGenre);
        }

        public async Task CreateMultiple(GenreResponseModel[] models)
        {
            //Genre[] genres = (Genre[])models.Select(x => new Genre(x));

            Genre[] genres = Array.ConvertAll(models, x => new Genre(x) );

            await repo.InsertOrUpdateMultipleAsync(genres);
        }

        public async Task<bool> Update(GenreModel genreModel)
        {
            var updateGenre = await repo.GetOneDbByIdAsync(genreModel.genre_id);
            if (updateGenre == null)
                return false;

            updateGenre.Copy(genreModel);

            return await repo.UpdateAsync(updateGenre);
        }

        public async Task<bool> Delete(int genre_id)
        {
            var delGenre = await repo.GetOneDbByIdAsync(genre_id);
            if (delGenre == null)
                return false;

            return await repo.DeleteAsync(delGenre);
        }

    }
}
