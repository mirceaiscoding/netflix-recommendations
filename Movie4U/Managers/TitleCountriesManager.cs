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
            if (config == null)
                config = new GetAllConfig<TitleCountry>();

            config.extraEntityFilters = new List<Func<IQueryable<TitleCountry>, IQueryable<TitleCountry>>>();
            config.extraEntityFilters.Add(source => source.propertyFilter("netflix_id", netflixId));

            return await repo.GetAllFromPageAsync(config);
        }

        public async Task<List<CountryModel>> GetAllCountriesByNetflixIdFromPageAsync(int netflixId, GetAllConfig<TitleCountry> config = null)
        {
            var titleCountries = await GetAllByNetflixIdFromPageAsync(netflixId, config);

            var countries = new List<CountryModel>();
            foreach (var titleCountry in titleCountries)
            {
                var country = await countriesManager.GetOneByIdAsync(titleCountry.country_id);
                countries.Add(country);
            }

            return countries;
        }

        public async Task CreateOrUpdateMultiple(TitleCountryModel[] models)
        {
            TitleCountry[] titlesCountries = Array.ConvertAll(models, x => new TitleCountry(x));

            await repo.InsertOrUpdateMultipleAsync(titlesCountries);
        }

    }
}
