using Movie4U.Configurations;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Repositories.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface ITitleGenresManager: IGenericManager<TitleGenre, TitleGenreModel, ITitleGenresRepository>
    {
        Task<List<TitleGenreModel>> GetAllByNetflixIdFromPageAsync(int netflixId, GetAllConfig<TitleGenre> config = null);

        Task<List<GenreModel>> GetAllGenresByNetflixIdFromPageAsync(int netflixId, GetAllConfig<TitleGenre> config = null);

        Task CreateOrUpdateMultiple(TitleGenreModel[] titleGenreModels);

    }
}
