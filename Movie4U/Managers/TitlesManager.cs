using Movie4U.Configurations;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Managers.IManagers;
using Movie4U.Repositories.IRepositories;
using Movie4U.Utilities;
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


        public new async Task<List<TitleModel>> GetAllFromPageAsync(GetAllConfig<Title> config = null)
        {
            if (config == null)
                config = new GetAllConfig<Title>();

            config.includers = includers;
            config.asSplitQuery = true;

            return await repo.GetAllFromPageAsync(config);
        }

        public async Task<TitleModel> GetOneByIdAsync(int netflix_id)
        {
            return await repo
                .GetOneByIdAsync(
                    GetOneConfigFactory<Title, TitleModel>.New(
                        new object[] { netflix_id },
                        includers,
                        true));
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
            var updateTitle = await repo
                .GetOneDbByIdAsync(
                    GetOneConfigFactory<Title, TitleModel>.New(
                        new object[] { titleModelParam.netflix_id },
                        includers,
                        true));

            if (updateTitle == null)
                return false;

            updateTitle.Copy(titleModelParam);

            return await repo.UpdateAsync(updateTitle);
        }

    }
}
