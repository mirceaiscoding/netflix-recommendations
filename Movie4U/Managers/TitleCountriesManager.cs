using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Managers.IManagers;
using Movie4U.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers
{
    public class TitleCountriesManager: ITitleCountriesManager
    {

        private readonly ITitleCountriesRepository repo;
        private readonly ICountriesManager countriesManager;

        /**<summary>
         * Constructor.
         * </summary>*/
        public TitleCountriesManager(ITitleCountriesRepository repo, ICountriesManager countriesManager)
        {
            this.repo = repo;
            this.countriesManager = countriesManager;
        }


        public async Task<List<TitleCountryModel>> GetAllFromPageAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1)
        {
            return await repo.GetAllFromPageAsync(orderByFlagsPacked, whereFlagsPacked, pageIndex);
        }

        public async Task<List<TitleCountryModel>> GetAllByNetflixIdFromPageAsync(int netflixId, int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1)
        {
            List<Func<TitleCountry, bool>> extraEntityFilters = new List<Func<TitleCountry, bool>>();
            extraEntityFilters.Add(tc => tc.netflix_id == netflixId);

            return await repo.GetAllFromPageAsync(orderByFlagsPacked, whereFlagsPacked, pageIndex, extraEntityFilters);
        }

        public async Task<List<CountryModel>> GetAllCountriesByNetflixIdFromPageAsync(int netflixId, int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1)
        {
            var titleCountries = await GetAllByNetflixIdFromPageAsync(netflixId);

            var countries = new List<CountryModel>();
            foreach (var titleCountry in titleCountries)
            {
                var country = await countriesManager.GetOneByIdAsync(titleCountry.country_id);
                countries.Add(country);
            }

            return countries;
        }

        public async Task<TitleCountryModel> GetOneByIdAsync(int country_id, int netflix_id)
        {
            return await repo.GetOneByIdAsync(country_id, netflix_id);
        }

        public async Task Create(TitleCountryModel titleCountryModel)
        {
            TitleCountry newTitleGenre = new TitleCountry(titleCountryModel);

            await repo.InsertAsync(newTitleGenre);
        }

        public async Task CreateOrUpdateMultiple(TitleCountryModel[] models)
        {
            TitleCountry[] titlesCountries = Array.ConvertAll(models, x => new TitleCountry(x));

            await repo.InsertOrUpdateMultipleAsync(titlesCountries);
        }

        public async Task Update(TitleCountryModel titleCountryModel)
        {
            TitleCountry updateTitleCountry = await repo.GetOneDbByIdAsync(titleCountryModel.country_id, titleCountryModel.netflix_id);
            updateTitleCountry.Copy(titleCountryModel);

            await repo.UpdateAsync(updateTitleCountry);
        }

        public async Task Delete(int country_id, int netflix_id)
        {
            TitleCountry delTitleCountry = await repo.GetOneDbByIdAsync(country_id, netflix_id);

            if (delTitleCountry != null)
                await repo.DeleteAsync(delTitleCountry);
        }

    }
}
