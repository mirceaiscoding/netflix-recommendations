using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.EntitiesModels.Models.uNoGS;
using Movie4U.Managers.IManagers;
using Movie4U.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers
{
    public class CountriesManager : ICountriesManager
    {
        private readonly ICountriesRepository repo;

        /**<summary>
         * Constructor.
         * </summary>*/
        public CountriesManager(ICountriesRepository repo)
        {
            this.repo = repo;
        }


        public async Task<List<CountryModel>> GetAllFromPageAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1)
        {
            return await repo.GetAllFromPageAsync(orderByFlagsPacked, whereFlagsPacked, pageIndex);
        }

        public async Task<CountryModel> GetOneByIdAsync(int country_id)
        {
            return await repo.GetOneByIdAsync(country_id);
        }

        public async Task Create(CountryModel countryModel)
        {
            var newCountry = new Country(countryModel);

            await repo.InsertAsync(newCountry);
        }

        public async Task CreateOrUpdateMultiple(CountryResponseModel[] models)
        {
            //Genre[] genres = (Genre[])models.Select(x => new Genre(x));

            Country[] countries = Array.ConvertAll(models, x => new Country(x));

            await repo.InsertOrUpdateMultipleAsync(countries);
        }

        public async Task<bool> Update(CountryModel countryModel)
        {
            var updateCountry = await repo.GetOneDbByIdAsync(countryModel.id);
            if(updateCountry == null)
                return false;

            updateCountry.Copy(countryModel);

            return await repo.UpdateAsync(updateCountry);
        }

        public async Task<bool> Delete(int country_id)
        {
            var delCountry = await repo.GetOneDbByIdAsync(country_id);
            if (delCountry == null)
                return false;

            return await repo.DeleteAsync(delCountry);
        }

    }
}
