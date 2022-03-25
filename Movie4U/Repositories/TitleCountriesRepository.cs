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
    public class TitleCountriesRepository: GenericRepository<TitleCountry, TitleCountryModel>, ITitleCountriesRepository
    {

        /**<summary>
        * Constructor.
        * </summary>>*/
        public TitleCountriesRepository(Movie4UContext db) : base(db) { }


        public async Task<List<TitleCountryModel>> GetAllAsync()
        {
            var titleCountries = await db.TitleCountries.ToListAsync();

            return CastUtility.ToModelsList<TitleCountry, TitleCountryModel>(titleCountries);
        }

        public async Task<List<TitleCountryModel>> GetAllByNetflixIdAsync(string netflixId)
        {
            var allDb = await GetAllDbAsync();
            var titleCountries =
                allDb
                .Where(tg => tg.netflix_id == netflixId)
                .ToList();

            return CastUtility.ToModelsList<TitleCountry, TitleCountryModel>(titleCountries);
        }

        public async Task<TitleCountryModel> GetOneByIdAsync(string country_code, string netflix_id)
        {
            var titleCountry = await
                db
                .TitleCountries
                .FirstOrDefaultAsync(tc => tc.country_code == country_code && tc.netflix_id == netflix_id);

            if (titleCountry != null)
                return new TitleCountryModel(titleCountry);

            return null;
        }

    }
}
