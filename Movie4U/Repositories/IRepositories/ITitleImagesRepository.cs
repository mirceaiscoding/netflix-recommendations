using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Repositories.IRepositories
{
    public interface ITitleImagesRepository: IGenericRepository<TitleImage>
    {
        Task<List<TitleImageModel>> GetAllAsync();

        List<TitleImageModel> GetAllByNetflixIdAsync(string netflixId);

        Task<TitleImageModel> GetOneByIdAsync(string url);
    }
}
