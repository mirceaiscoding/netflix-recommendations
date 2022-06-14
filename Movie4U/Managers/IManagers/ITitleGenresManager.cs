using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface ITitleGenresManager
    {
        Task<List<TitleGenreModel>> GetAllFromPageAsync(GetAllConfig<TitleGenre> config = null);

        Task<List<TitleGenreModel>> GetAllByNetflixIdFromPageAsync(int netflixId, GetAllConfig<TitleGenre> config = null);

        Task<List<GenreModel>> GetAllGenresByNetflixIdFromPageAsync(int netflixId, GetAllConfig<TitleGenre> config = null);

        Task<TitleGenreModel> GetOneByIdAsync(int genre_id, int netflix_id);

        Task<bool> Update(TitleGenreModel titleGenreModel);

        Task Create(TitleGenreModel titleGenreModel);
        Task CreateOrUpdateMultiple(TitleGenreModel[] titleGenreModels);

        Task<bool> Delete(int genre_id, int netflix_id);
    }
}
