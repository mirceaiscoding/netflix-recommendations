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

        [HttpPost("CreateGenres")]
        //[Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateGenres()
        {
            await databasePopulatorService.CreateGenresAsync();
            return Ok();
        }
    }
}
