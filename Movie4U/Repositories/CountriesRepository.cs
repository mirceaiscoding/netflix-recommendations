using Microsoft.EntityFrameworkCore;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Repositories.IRepositories;
using Movie4U.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Repositories
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        /**<summary>
         * Constructor.
         * </summary>*/
        public CountriesRepository(Movie4UContext db) : base(db) { }


        public async Task<List<CountryModel>> GetAllAsync()
        {
            var countries = await db.Countries.ToListAsync();

            return CastUtility.ToModelsList<Country, CountryModel>(countries);
        }

        public async Task<CountryModel> GetOneByIdAsync(string country_code)
        {
            var country = await GetOneDbByIdAsync(country_code);

            if (country != null)
                return new CountryModel(country);

            return null;
        }

    }
}
