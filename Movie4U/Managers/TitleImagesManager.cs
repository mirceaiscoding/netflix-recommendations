﻿using Movie4U.EntitiesModels.Entities;
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
            TitleImage newTitleImage = new TitleImage(titleImageModel);

            await repo.InsertAsync(newTitleImage);
        }

        public async Task Update(TitleImageModel titleImageModel)
        {
            TitleImage updateTitleImage = await repo.GetOneDbByIdAsync(titleImageModel.url);
            updateTitleImage.Copy(titleImageModel);

            await repo.UpdateAsync(updateTitleImage);
        }

        public async Task Delete(string url)
        {
            TitleImage delTitleImage = await repo.GetOneDbByIdAsync(url);

            if (delTitleImage != null)
                await repo.DeleteAsync(delTitleImage);
        }

    }
}
