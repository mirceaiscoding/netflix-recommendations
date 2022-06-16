﻿using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Managers.IManagers;
using Movie4U.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Movie4U.Managers
{
    public class TitlesManager : GenericManager<Title, TitleModel, ITitlesRepository>, ITitlesManager
    {
        public static Expression<Func<Title, object>>[] includers;

        static TitlesManager()
        {
            includers = new Expression<Func<Title, object>>[]
            {
                title => title.titleCountries,
                title => title.titleGenres,
                title => title.titleImages
            };
        }

        private readonly ITitleCountriesManager titleCountriesManager;
        private readonly ITitleGenresManager titleGenresManager;
        private readonly ITitleImagesManager titleImagesManager; 

        /**<summary>
         * Constructor.
         * </summary>*/
        public TitlesManager(ITitlesRepository repo, ITitleCountriesManager countriesManager, ITitleGenresManager genresManager, ITitleImagesManager imagesManager): base(repo)
        {
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

        public new async Task<List<TitleModel>> GetAllFromPageAsync(GetAllConfig<Title> config = null)
        {
            Func<List<TitleModel>, Task> filler = async titleModels =>
            {
                foreach (var titleModel in titleModels)
                    await FillModelsLists(titleModel);
            };

            if (config == null)
                config = new GetAllConfig<Title>();

            config.includers = includers;

            return await repo.GetAllFromPageAsync(config, null, filler);
        }

        public async Task<TitleModel> GetOneByIdAsync(int netflix_id)
        {
            Func<TitleModel, Task> filler = async titleModel =>
                await FillModelsLists(titleModel);

            return await repo.GetOneByIdAsync(netflix_id, filler);
        }

        public async Task Create(TitleModelParameter titleModelParam)
        {
            var newTitle = new Title(titleModelParam);

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

        public async Task<bool> Update(TitleModelParameter titleModelParam)
        {
            var updateTitle = await repo.GetOneDbByIdAsync(titleModelParam.netflix_id);
            if (updateTitle == null)
                return false;

            updateTitle.Copy(titleModelParam);

            return await repo.UpdateAsync(updateTitle);
        }

    }
}
