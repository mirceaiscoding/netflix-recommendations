using Movie4U.Entities;
using Movie4U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Repositories.IRepositories
{
    public interface ICountriesRepository : IGenericRepository<Country>
    {
        Task<List<CountryModel>> GetAllAsync();

        Task<CountryModel> GetOneByIdAsync(string country_code);
    }
}
