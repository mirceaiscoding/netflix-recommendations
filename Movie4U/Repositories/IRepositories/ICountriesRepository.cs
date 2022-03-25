using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Repositories.IRepositories
{
    public interface ICountriesRepository : IGenericRepository<Country, CountryModel>
    {
        Task<List<CountryModel>> GetAllAsync();

        Task<CountryModel> GetOneByIdAsync(string country_code);
    }
}
