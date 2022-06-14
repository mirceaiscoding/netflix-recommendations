using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Managers.IManagers;
using System.Threading.Tasks;

namespace Movie4U.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesManager manager;

        public CountriesController(ICountriesManager manager)
        {
            this.manager = manager;
        }

        [HttpGet("GetAllFromPage/{pageIndex}")]
        [Authorize(Policy = "BasicUserPolicy")]
        public async Task<IActionResult> GetAllCountriesFromPageAsync([FromHeader] int orderByFlagsPacked = 0, [FromHeader] int whereFlagsPacked = 0, [FromRoute] int? pageIndex = 1)
        {
            var countries = await manager.GetAllFromPageAsync(new GetAllConfig<Country>(orderByFlagsPacked, whereFlagsPacked, pageIndex));
            
            if (countries.Count == 0)
                return NotFound("There are no countries stored in the database");

            return Ok(countries);
        }

        [HttpGet("GetOneById/{id}")]
        [Authorize(Policy = "BasicUserPolicy")]
        public async Task<IActionResult> GetCountryByIdAsync([FromRoute] int id)
        {
            var country = await manager.GetOneByIdAsync(id);

            if (country == null)
                return NotFound("There is no country with the given code stored in the database");

            return Ok(country);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateCountryAsync([FromBody] CountryModel countryModel)
        {
            await manager.Create(countryModel);
            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateCountryAsync([FromBody] CountryModel countryModel)
        {
            await manager.Update(countryModel);
            return Ok();
        }


        [HttpDelete]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteCountryAsync([FromBody] int country_id)
        {
            await manager.Delete(country_id);
            return Ok();
        }

    }
}
