using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface ITitleCountriesManager
    {
        Task<List<TitleCountryModel>> GetAllFromPageAsync(GetAllConfig<TitleCountry> config = null);

        Task<List<TitleCountryModel>> GetAllByNetflixIdFromPageAsync(int netflixId, GetAllConfig<TitleCountry> config = null);

        Task<List<CountryModel>> GetAllCountriesByNetflixIdFromPageAsync(int netflixId, GetAllConfig<TitleCountry> config = null);

        Task<TitleCountryModel> GetOneByIdAsync(int country_id, int netflix_id);

        Task<bool> Update(TitleCountryModel titleCountryModel);

        Task Create(TitleCountryModel titleCountryModel);

        Task CreateOrUpdateMultiple(TitleCountryModel[] titleCountryModels);

        Task<bool> Delete(int country_id, int netflix_id);
    }
}
