using Movie4U.EntitiesModels.Models;
using Movie4U.EntitiesModels.Models.uNoGS;
using Movie4U.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface ICountriesManager
    {
        Task<List<CountryModel>> GetAllFromPageAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1);

        Task<CountryModel> GetOneByIdAsync(int country_id);

        Task Create(CountryModel countryModel);

        Task CreateMultiple(CountryResponseModel[] models);

        Task Update(CountryModel countryModel);

        Task Delete(int country_id);
    }
}
