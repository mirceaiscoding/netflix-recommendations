using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.EntitiesModels.Models.uNoGS;
using Movie4U.Repositories.IRepositories;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface ICountriesManager: IGenericManager<Country, CountryModel, ICountriesRepository>
    {
        Task CreateOrUpdateMultiple(CountryResponseModel[] models);
    }
}
