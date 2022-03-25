using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Repositories.IRepositories
{
    public interface ITitleImagesRepository: IGenericRepository<TitleImage, TitleImageModel>
    {
        Task<List<TitleImageModel>> GetAllAsync();

        Task<List<TitleImageModel>> GetAllByNetflixIdAsync(string netflixId);

        Task<TitleImageModel> GetOneByIdAsync(string url);
    }
}
