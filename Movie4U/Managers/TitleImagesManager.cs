using Movie4U.Configurations;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Managers.IManagers;
using Movie4U.Repositories.IRepositories;
using Movie4U.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers
{
    public class TitleImagesManager: GenericManager<TitleImage, TitleImageModel, ITitleImagesRepository>, ITitleImagesManager
    {
        /**<summary>
         * Constructor.
         * </summary>*/
        public TitleImagesManager(ITitleImagesRepository repo) : base(repo) { }


        public async Task<List<TitleImageModel>> GetAllByNetflixIdFromPageAsync(int netflixId, GetAllConfig<TitleImage> config = null)
        {
            return await repo.GetAllFromPageAsync(
                NetflixIdDependentsUtility<TitleImage>
                .AddNetflixIdExtraEntityFilter(
                    netflixId,
                    config));
        }

    }
}
