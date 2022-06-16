using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie4U.Configurations;
using Movie4U.EntitiesModels.Entities;
using Movie4U.Managers;
using Movie4U.Managers.IManagers;
using System.Threading.Tasks;

namespace Movie4U.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WatchersController : ControllerBase
    {
        private readonly IWatchersManager manager;

        /**<summary>
         * Constructor.
         * </summary>*/
        public WatchersController(IWatchersManager manager)
        {
            this.manager = manager;
        }

        [HttpGet("GetAllFromPage/{pageIndex}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetAllWatchersFromPageAsync([FromHeader] int orderByFlagsPacked = 0, [FromHeader] int whereFlagsPacked = 0, [FromRoute] int? pageIndex = 1)
        {
            var watchers = await manager.GetAllFromPageAsync(new GetAllConfig<Watcher>(orderByFlagsPacked, whereFlagsPacked, pageIndex));

            if (watchers.Count == 0)
                return NotFound("There are no watchers stored in the database");

            return Ok(watchers);
        }

        [HttpGet("GetOneByName/{name}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetWatcherByNameAsync([FromRoute] string name)
        {
            var watcher = await manager.GetOneByIdAsync(name);

            if (watcher == null)
                return NotFound("There is no watcher with the given name stored in the database");

            return Ok(watcher);
        }

        [HttpPut("UpdateWatcherCountryId")]
        [Authorize(Policy = "BasicUserPolicy")]
        public async Task<IActionResult> UpdateWatcherCountryId([FromHeader] string Authorization, [FromBody] int? countryId)
        {
            var watcherName = TokensManager.ExtractUserName(Authorization);

            var watcherModel = await manager.GetOneByIdAsync(watcherName);
            if (watcherModel == null)
                return BadRequest("The watcher couldn not be found");

            await manager.UpdateWatcherCountryId(watcherName, countryId);

            return Ok();
        }



    }
}
