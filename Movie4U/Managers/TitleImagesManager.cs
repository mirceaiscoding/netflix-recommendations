using Movie4U.Configurations;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.ExtensionMethods;
using Movie4U.Managers.IManagers;
using Movie4U.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (config == null)
                config = new GetAllConfig<TitleImage>();

            config.extraEntityFilters = new List<Func<IQueryable<TitleImage>, IQueryable<TitleImage>>>();
            config.extraEntityFilters.Add(source => source.PropertyFilter("netflix_id", netflixId));

            return await repo.GetAllFromPageAsync(config);
        }

    }
}
