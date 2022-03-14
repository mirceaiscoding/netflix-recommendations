using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Repositories.IRepositories
{
    public interface ITitleCountriesRepository: IGenericRepository<TitleCountry>
    {
        Task<List<TitleCountryModel>> GetAllAsync();

        Task<List<TitleCountryModel>> GetAllByNetflixIdAsync(string netflixId);

        Task<TitleCountryModel> GetOneByIdAsync(string country_code, string netflix_id);
    }
}
