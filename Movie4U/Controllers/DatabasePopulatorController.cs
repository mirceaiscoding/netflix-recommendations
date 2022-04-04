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

        [HttpPost("UpdateCountries")]
        //[Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateCountries()
        {
            await databasePopulatorService.updateCountriesAsync();
            return Ok();
        }
    }
}
