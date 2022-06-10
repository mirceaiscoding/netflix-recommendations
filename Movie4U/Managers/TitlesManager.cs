using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Managers.IManagers;
using Movie4U.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers
{
    public class TitlesManager: ITitlesManager
    {
        private readonly ITitlesRepository repo;
        private readonly ITitleCountriesManager titleCountriesManager;
        private readonly ITitleGenresManager titleGenresManager;
        private readonly ITitleImagesManager titleImagesManager; 

        /**<summary>
         * Constructor.
         * </summary>*/
        public TitlesManager(ITitlesRepository repo, ITitleCountriesManager countriesManager, ITitleGenresManager genresManager, ITitleImagesManager imagesManager)
        {
            this.repo = repo;
            this.titleCountriesManager = countriesManager;
            this.titleGenresManager = genresManager;
            this.titleImagesManager = imagesManager;
        }


        private async Task<bool> FillModelsLists(TitleModel titleModel)
        {
            if(titleModel == null)
                return false;

            var netflixId = titleModel.netflix_id;
            titleModel.countryModels = await titleCountriesManager.GetAllCountriesByNetflixIdFromPageAsync(netflixId);
            titleModel.genreModels = await titleGenresManager.GetAllGenresByNetflixIdFromPageAsync(netflixId);
            titleModel.titleImageModels = await titleImagesManager.GetAllByNetflixIdFromPageAsync(netflixId);

            return true;
        }

        public async Task<List<TitleModel>> GetAllFromPageAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1)
        {
            Func<List<TitleModel>, Task> filler = async titleModels =>
            {
                foreach (var titleModel in titleModels)
                    await FillModelsLists(titleModel);
            };

            return await repo.GetAllFromPageAsync(orderByFlagsPacked, whereFlagsPacked, pageIndex, null, null, filler);
        }

        public async Task<TitleModel> GetOneByIdAsync(int netflix_id)
        {
            Func<TitleModel, Task> filler = async titleModel =>
                await FillModelsLists(titleModel);

            return await repo.GetOneByIdAsync(netflix_id, filler);
        }

        public async Task Create(TitleModelParameter titleModelParam)
        {
            Title newTitle = new Title(titleModelParam);

            await repo.InsertAsync(newTitle);
        }

        public async Task CreateMultiple(TitleModel[] models)
        {
            Title[] titles = Array.ConvertAll(models, x => new Title(x));

            await repo.InsertMultipleAsync(titles);
        }

        public async Task CreateOrUpdateMultiple(TitleModel[] models)
        {
            Title[] titles = Array.ConvertAll(models, x => new Title(x));

            await repo.InsertOrUpdateMultipleAsync(titles);
        }

        public async Task Update(TitleModelParameter titleModelParam)
        {
            Title updateTitle = await repo.GetOneDbByIdAsync(titleModelParam.netflix_id);
            updateTitle.Copy(titleModelParam);

            await repo.UpdateAsync(updateTitle);
        }

        public async Task Delete(int netflix_id)
        {
            Title delTitle = await repo.GetOneDbByIdAsync(netflix_id);

            if (delTitle != null)
                await repo.DeleteAsync(delTitle);
        }

    }
}
