using Movie4U.Configurations;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Repositories.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface ITitleImagesManager: IGenericManager<TitleImage, TitleImageModel, ITitleImagesRepository>
    {
        Task<List<TitleImageModel>> GetAllByNetflixIdFromPageAsync(int netflixId, GetAllConfig<TitleImage> config = null);

    }
}
