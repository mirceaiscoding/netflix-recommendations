using Movie4U.EntitiesModels.Models;
using Movie4U.EntitiesModels.Models.uNoGS;
using Movie4U.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface IGenresManager
    {
        Task<List<GenreModel>> GetAllFromPageAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1);

        Task<GenreModel> GetOneByIdAsync(int genre_id);

        Task<bool> Update(GenreModel genreModel);

        Task Create(GenreModel genreModel);

        Task CreateMultiple(GenreResponseModel[] models);

        Task<bool> Delete(int genre_id);
    }
}
