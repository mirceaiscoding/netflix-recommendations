using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.EntitiesModels.Models.uNoGS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface ICountriesManager
    {
        Task<List<CountryModel>> GetAllFromPageAsync(GetAllConfig<Country> config = null);

        Task<CountryModel> GetOneByIdAsync(int country_id);

        Task Create(CountryModel countryModel);

        Task CreateOrUpdateMultiple(CountryResponseModel[] models);

        Task<bool> Update(CountryModel countryModel);

        Task<bool> Delete(int country_id);
    }
}
