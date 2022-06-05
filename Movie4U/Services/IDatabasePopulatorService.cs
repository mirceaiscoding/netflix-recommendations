using System;
using System.Threading.Tasks;

namespace Movie4U.Services
{
    public interface IDatabasePopulatorService
    {
        public Task CreateGenresAsync();

        public Task CreateCountriesAsync();

        // Will add to the database 100 titles from the specified country_id
        public Task CreateTitlesAsync(int country_id, int page_number);
    }
}
