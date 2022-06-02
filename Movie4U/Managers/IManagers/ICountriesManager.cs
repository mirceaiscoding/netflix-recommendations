using Movie4U.EntitiesModels.Models;
using Movie4U.EntitiesModels.Models.uNoGS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface ICountriesManager
    {
        Task<List<CountryModel>> GetAllAsync();

        Task<CountryModel> GetOneByIdAsync(int id);

        Task Create(CountryModel countryModel);

        Task CreateMultiple(CountryResponseModel[] models);

        Task Update(CountryModel countryModel);

        Task Delete(string country_code);
    }
}
