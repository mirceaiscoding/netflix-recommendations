using Movie4U.EntitiesModels.Models;
using Movie4U.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface ITitleImagesManager
    {
        Task<List<TitleImageModel>> GetAllFromPageAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1);

        Task<List<TitleImageModel>> GetAllByNetflixIdFromPageAsync(int netflixId, int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1);

        Task<TitleImageModel> GetOneByIdAsync(string url);

        Task<bool> Update(TitleImageModel titleImageModel);

        Task Create(TitleImageModel titleImageModel);

        Task<bool> Delete(string url);
    }
}
