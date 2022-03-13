using Movie4U.EntitiesModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface ITitleImagesManager
    {
        Task<List<TitleImageModel>> GetAllAsync();

        Task<TitleImageModel> GetOneByIdAsync(string url);

        Task Update(TitleImageModel titleImageModel);

        Task Create(TitleImageModel titleImageModel);

        Task Delete(string url);
    }
}
