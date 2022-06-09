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

        // Will add 100 genres to a title (most titles do not have more then 100 genres so only call once)
        public Task CreateTitleGenresAsync(int netflix_id, int page_number=0);
    }
}
