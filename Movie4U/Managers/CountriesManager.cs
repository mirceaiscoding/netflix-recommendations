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


        public async Task<List<CountryModel>> GetAllAsync()
        {
            return await repo.GetAllAsync();
        }

        public async Task<CountryModel> GetOneByIdAsync(int id)
        {
            return await repo.GetOneByIdAsync(id);
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
            Country updateCountry = await repo.GetOneDbByIdAsync(countryModel.countrycode);
            updateCountry.Copy(countryModel);

            await repo.UpdateAsync(updateCountry);
        }

        public async Task Delete(string country_code)
        {
            Country delCountry = await repo.GetOneDbByIdAsync(country_code);

            if (delCountry != null)
                await repo.DeleteAsync(delCountry);
        }

    }
}
