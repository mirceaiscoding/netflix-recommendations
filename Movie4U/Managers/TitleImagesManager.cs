using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Managers.IManagers;
using Movie4U.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers
{
    public class TitleImagesManager: ITitleImagesManager
    {
        private readonly ITitleImagesRepository repo;

        /**<summary>
         * Constructor.
         * </summary>*/
        public TitleImagesManager(ITitleImagesRepository repo)
        {
            this.repo = repo;
        }


        public async Task<List<TitleImageModel>> GetAllFromPageAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1)
        {
            return await repo.GetAllFromPageAsync(orderByFlagsPacked, whereFlagsPacked, pageIndex);
        }

        public async Task<List<TitleImageModel>> GetAllByNetflixIdFromPageAsync(int netflixId, int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1)
        {
            List<Func<TitleImage, bool>> extraEntityFilters = new List<Func<TitleImage, bool>>();
            extraEntityFilters.Add(ti => ti.netflix_id == netflixId);

            return await repo.GetAllFromPageAsync(orderByFlagsPacked, whereFlagsPacked, pageIndex, extraEntityFilters);
        }

        public async Task<TitleImageModel> GetOneByIdAsync(string url)
        {
            return await repo.GetOneByIdAsync(url);
        }

        public async Task Create(TitleImageModel titleImageModel)
        {
            var newTitleImage = new TitleImage(titleImageModel);

            await repo.InsertAsync(newTitleImage);
        }

        public async Task<bool> Update(TitleImageModel titleImageModel)
        {
            var updateTitleImage = await repo.GetOneDbByIdAsync(titleImageModel.url);
            if(updateTitleImage == null)
                return false;

            updateTitleImage.Copy(titleImageModel);

            return await repo.UpdateAsync(updateTitleImage);
        }

        public async Task<bool> Delete(string url)
        {
            var delTitleImage = await repo.GetOneDbByIdAsync(url);
            if (delTitleImage == null)
                return false;

            return await repo.DeleteAsync(delTitleImage);
        }

    }
}
