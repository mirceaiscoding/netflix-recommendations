using Movie4U.EntitiesModels.Models;
using Movie4U.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface ITitleCountriesManager
    {
        Task<List<TitleCountryModel>> GetAllAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageNumber = 1);

        Task<List<TitleCountryModel>> GetAllByNetflixIdAsync(string netflixId);

        Task<List<CountryModel>> GetAllCountriesByNetflixIdAsync(string netflixId);

        Task<TitleCountryModel> GetOneByIdAsync(string country_code, string netflix_id);

        Task Update(TitleCountryModel titleCountryModel);

        Task Create(TitleCountryModel titleCountryModel);

        Task Delete(string country_code, string netflix_id);
    }
}
