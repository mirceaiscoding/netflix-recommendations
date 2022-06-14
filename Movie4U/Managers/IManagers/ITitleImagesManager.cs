using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface ITitleImagesManager
    {
        Task<List<TitleImageModel>> GetAllFromPageAsync(GetAllConfig<TitleImage> config = null);

        Task<List<TitleImageModel>> GetAllByNetflixIdFromPageAsync(int netflixId, GetAllConfig<TitleImage> config = null);

        Task<TitleImageModel> GetOneByIdAsync(string url);

        Task<bool> Update(TitleImageModel titleImageModel);

        Task Create(TitleImageModel titleImageModel);

        Task<bool> Delete(string url);
    }
}
