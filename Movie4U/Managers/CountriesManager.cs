using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.EntitiesModels.Models.uNoGS;
using Movie4U.Enums;
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
            Country newCountry = new Country(countryModel);

            await repo.InsertAsync(newCountry);
        }

        public async Task CreateMultiple(CountryResponseModel[] models)
        {
            //Genre[] genres = (Genre[])models.Select(x => new Genre(x));

            Country[] countries = Array.ConvertAll(models, x => new Country(x));

            await repo.InsertMultipleAsync(countries);
        }

        public async Task Update(CountryModel countryModel)
        {
            Country updateCountry = await repo.GetOneDbByIdAsync(countryModel.id);
            updateCountry.Copy(countryModel);

            await repo.UpdateAsync(updateCountry);
        }

        public async Task Delete(int country_id)
        {
            Country delCountry = await repo.GetOneDbByIdAsync(country_id);

            if (delCountry != null)
                await repo.DeleteAsync(delCountry);
        }

    }
}
