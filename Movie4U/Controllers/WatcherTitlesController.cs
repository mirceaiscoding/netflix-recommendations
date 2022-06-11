using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie4U.EntitiesModels.Models;
using Movie4U.Managers;
using Movie4U.Managers.IManagers;
using System.Threading.Tasks;

namespace Movie4U.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WatcherTitlesController : ControllerBase
    {
        private readonly IWatcherTitlesManager manager;
        private readonly IWatchersManager watchersManager;

        public WatcherTitlesController(IWatcherTitlesManager manager, IWatchersManager watchersManager)
        {
            this.manager = manager;
            this.watchersManager = watchersManager;
        }

        [HttpGet("GetAllFromPage/{pageIndex}")]
        [Authorize(Policy = "BasicUserPolicy")]
        public async Task<IActionResult> GetAllWatcherTitlesFromPageAsync([FromHeader] string Authorization, [FromHeader] int orderByFlagsPacked = 0, [FromHeader] int whereFlagsPacked = 0, [FromRoute] int? pageIndex = 1)
        {
            var watcherName = TokensManager.ExtractUserName(Authorization);

            var watcherModel = await watchersManager.GetOneByIdAsync(watcherName);
            if (watcherModel == null)
                return BadRequest("The watcher couldn not be found");

            var watcherTitles = await manager.GetAllFromPageAsync(orderByFlagsPacked, whereFlagsPacked, pageIndex, watcherModel);

            if (watcherTitles.Count == 0)
                return NotFound("There are no watcher titles stored in the database");

            return Ok(watcherTitles);
        }

        [HttpGet("GetOneById/{watcher_name}/{netflix_id}")]
        [Authorize(Policy = "BasicUserPolicy")]
        public async Task<IActionResult> GetWatcherTitleByIdAsync([FromHeader] string Authorization, int netflix_id)
        {
            var watcherName = TokensManager.ExtractUserName(Authorization);

            var watcherModel = await watchersManager.GetOneByIdAsync(watcherName);
            if (watcherModel == null)
                return BadRequest("The watcher couldn not be found");

            var watcherTitle = await manager.GetOneByIdAsync(watcherName, netflix_id);

            if (watcherTitle == null)
                return NotFound("There is no watcher title with the given id stored in the database");

            return Ok(watcherTitle);
        }

        [HttpPost]
        [Authorize(Policy = "BasicUserPolicy")]
        public async Task<IActionResult> CreateWatcherTitleAsync([FromHeader] string Authorization, [FromBody] WatcherTitleModelParameter watcherTitleModelParam)
        {
            var watcherName = TokensManager.ExtractUserName(Authorization);

            var watcherModel = await watchersManager.GetOneByIdAsync(watcherName);
            if (watcherModel == null)
                return BadRequest("The watcher couldn not be found");

            watcherTitleModelParam.watcher_name = watcherName;
            await manager.Create(watcherTitleModelParam);
            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = "BasicUserPolicy")]
        public async Task<IActionResult> UpdateWatcherTitleAsync([FromHeader] string Authorization, [FromBody] WatcherTitleModelParameter watcherTitleModelParam)
        {
            var watcherName = TokensManager.ExtractUserName(Authorization);

            var watcherModel = await watchersManager.GetOneByIdAsync(watcherName);
            if (watcherModel == null)
                return BadRequest("The watcher couldn not be found");

            watcherTitleModelParam.watcher_name = watcherName;
            await manager.Update(watcherTitleModelParam);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteWatcherTitleAsync([FromHeader] string Authorization, [FromBody] string watcher_name, int netflix_id)
        {
            var watcherName = TokensManager.ExtractUserName(Authorization);

            var watcherModel = await watchersManager.GetOneByIdAsync(watcherName);
            if (watcherModel == null)
                return BadRequest("The watcher couldn not be found");

            await manager.Delete(watcher_name, netflix_id);
            return Ok();
        }

    }
}