using Microsoft.EntityFrameworkCore;
using Movie4U.Entities;
using Movie4U.Models;
using Movie4U.Repositories.IRepositories;
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
        public CountriesRepository(Movie4UContext db) : base(db)
        {
            this.db = db;
        }

        public async Task<List<CountryModel>> GetAllAsync()
        {
            var countries = await db.Countries.ToListAsync();

            var countryModels = new List<CountryModel>();
            foreach (var country in countries)
            {
                var countryModel = new CountryModel();
                countryModel.Copy(country);

                countryModels.Add(countryModel);
            }

            return countryModels;
        }

        public async Task<CountryModel> GetOneByIdAsync(string country_code)
        {
            var country = await GetOneDbByIdAsync(country_code);

            if (country != null)
            {
                var countryModel = new CountryModel();
                countryModel.Copy(country);

                return countryModel;
            }

            return null;
        }

    }
}
