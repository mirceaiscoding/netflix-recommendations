using Movie4U.EntitiesModels.Models;
using Movie4U.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface ITitleCountriesManager
    {
        Task<List<TitleCountryModel>> GetAllFromPageAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1);

        Task<List<TitleCountryModel>> GetAllByNetflixIdFromPageAsync(int netflixId, int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1);

        Task<List<CountryModel>> GetAllCountriesByNetflixIdFromPageAsync(int netflixId, int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1);

        Task<TitleCountryModel> GetOneByIdAsync(int country_id, int netflix_id);

        Task Update(TitleCountryModel titleCountryModel);

        Task Create(TitleCountryModel titleCountryModel);

        Task CreateOrUpdateMultiple(TitleCountryModel[] titleCountryModels);

        Task Delete(int country_id, int netflix_id);
    }
}
