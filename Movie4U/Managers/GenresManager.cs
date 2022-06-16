using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.EntitiesModels.Models.uNoGS;
using Movie4U.Managers.IManagers;
using Movie4U.Repositories.IRepositories;
using System;
using System.Threading.Tasks;

namespace Movie4U.Managers
{
    public class GenresManager : GenericManager<Genre, GenreModel, IGenresRepository>, IGenresManager
    {
        /**<summary>
         * Constructor.
         * </summary>*/
        public GenresManager(IGenresRepository repo) : base(repo) { }


        public async Task CreateMultiple(GenreResponseModel[] models)
        {
            //Genre[] genres = (Genre[])models.Select(x => new Genre(x));

            Genre[] genres = Array.ConvertAll(models, x => new Genre(x) );

            await repo.InsertOrUpdateMultipleAsync(genres);
        }

    }
}
