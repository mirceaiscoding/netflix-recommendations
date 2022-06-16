using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.EntitiesModels.Models.uNoGS;
using Movie4U.Managers.IManagers;
using Movie4U.Repositories.IRepositories;
using System;
using System.Threading.Tasks;

namespace Movie4U.Managers
{
    public class CountriesManager : GenericManager<Country, CountryModel, ICountriesRepository>, ICountriesManager
    {
        /**<summary>
         * Constructor.
         * </summary>*/
        public CountriesManager(ICountriesRepository repo) : base(repo) { }


        public async Task CreateOrUpdateMultiple(CountryResponseModel[] models)
        {
            //Genre[] genres = (Genre[])models.Select(x => new Genre(x));

            Country[] countries = Array.ConvertAll(models, x => new Country(x));

            await repo.InsertOrUpdateMultipleAsync(countries);
        }

    }
}
