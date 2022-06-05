using Movie4U.EntitiesModels.Models;
using Movie4U.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface ITitleGenresManager
    {
        Task<List<TitleGenreModel>> GetAllFromPageAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1);

        Task<List<TitleGenreModel>> GetAllByNetflixIdFromPageAsync(int netflixId, int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1);

        Task<List<GenreModel>> GetAllGenresByNetflixIdFromPageAsync(int netflixId, int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1);

        Task<TitleGenreModel> GetOneByIdAsync(int genre_id, int netflix_id);

        Task Update(TitleGenreModel titleGenreModel);

        Task Create(TitleGenreModel titleGenreModel);

        Task Delete(int genre_id, int netflix_id);
    }
}
