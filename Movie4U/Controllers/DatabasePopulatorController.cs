using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie4U.Services;

namespace Movie4U.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatabasePopulatorController : ControllerBase
    {
        private readonly IDatabasePopulatorService databasePopulatorService;


        public DatabasePopulatorController(IDatabasePopulatorService databasePopulatorService)
        {
            this.databasePopulatorService = databasePopulatorService;
        }

        [HttpPost("PopulateGenres")]
        //[Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateGenres()
        {
            await databasePopulatorService.CreateGenresAsync();
            return Ok();
        }

        [HttpPost("PopulateCountries")]
        //[Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateCountries()
        {
            await databasePopulatorService.CreateCountriesAsync();
            return Ok();
        }

        [HttpPost("PopulateTitlesForCountry/{country_id}/{pageIndex}")]
        public async Task<IActionResult> CreateCountryTitles([FromRoute] int country_id = 400, [FromRoute] int pageIndex = 0)
        {
            await databasePopulatorService.CreateTitlesAsync(country_id, pageIndex);
            return Ok();
        }

        [HttpPost("PopulateGenresForTitle/{netflix_id}")]
        public async Task<IActionResult> CreateGenresForTitle([FromRoute] int netflix_id = 70076332)
        {
            await databasePopulatorService.CreateTitleGenresAsync(netflix_id);
            return Ok();
        }
    }
}
