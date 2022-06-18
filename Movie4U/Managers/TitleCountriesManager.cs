using Movie4U.Configurations;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Managers.IManagers;
using Movie4U.Repositories.IRepositories;
using Movie4U.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Managers
{
    public class TitleCountriesManager: GenericManager<TitleCountry, TitleCountryModel, ITitleCountriesRepository>, ITitleCountriesManager
    {
        private readonly ICountriesManager countriesManager;

        /**<summary>
         * Constructor.
         * </summary>*/
        public TitleCountriesManager(ITitleCountriesRepository repo, ICountriesManager countriesManager): base(repo)
        {
            this.countriesManager = countriesManager;
        }

        public async Task<List<TitleCountryModel>> GetAllByNetflixIdFromPageAsync(int netflixId, GetAllConfig<TitleCountry> config = null)
        {
            return await repo.GetAllFromPageAsync(
                NetflixIdDependentsUtility<TitleCountry>
                .AddNetflixIdExtraEntityFilter(
                    netflixId,
                    config));
        }

        public async Task<List<CountryModel>> GetAllCountriesByNetflixIdFromPageAsync(int netflixId, GetAllConfig<TitleCountry> config = null)
        {
            var tasks =
                (await GetAllByNetflixIdFromPageAsync(netflixId, config))
                .Select(
                    async tcm => new CountryModel(
                        await countriesManager.GetOneByIdAsync(tcm.country_id)));

            return (await Task.WhenAll(tasks))
                .ToList();
        }

        public async Task CreateOrUpdateMultiple(TitleCountryModel[] models)
        {
            TitleCountry[] titlesCountries = Array.ConvertAll(models, x => new TitleCountry(x));

            await repo.InsertOrUpdateMultipleAsync(titlesCountries);
        }

    }
}
