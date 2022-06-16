using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Repositories.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface ITitleCountriesManager: IGenericManager<TitleCountry, TitleCountryModel, ITitleCountriesRepository>
    {
        Task<List<TitleCountryModel>> GetAllByNetflixIdFromPageAsync(int netflixId, GetAllConfig<TitleCountry> config = null);

        Task<List<CountryModel>> GetAllCountriesByNetflixIdFromPageAsync(int netflixId, GetAllConfig<TitleCountry> config = null);

        Task CreateOrUpdateMultiple(TitleCountryModel[] titleCountryModels);
    }
}
