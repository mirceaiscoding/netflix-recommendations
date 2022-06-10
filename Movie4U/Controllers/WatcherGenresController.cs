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
    public class WatcherGenresController : ControllerBase
    {
        private readonly IWatcherGenresManager manager;
        private readonly IWatchersManager watchersManager;

        public WatcherGenresController(IWatcherGenresManager manager, IWatchersManager watchersManager)
        {
            this.manager = manager;
            this.watchersManager = watchersManager;
        }

        [HttpGet("GetAllFromPage/{pageIndex}")]
        [Authorize(Policy = "BasicUserPolicy")]
        public async Task<IActionResult> GetAllWatcherGenresFromPageAsync([FromHeader] string Authorization, [FromHeader] int orderByFlagsPacked = 0, [FromHeader] int whereFlagsPacked = 0, [FromRoute] int? pageIndex = 1)
        {
            var watcherName = TokensManager.ExtractUserName(Authorization);

            var watcherModel = await watchersManager.GetOneByIdAsync(watcherName);
            if (watcherModel == null)
                return BadRequest("The watcher couldn not be found");

            var watcherTitles = await manager.GetAllFromPageAsync(orderByFlagsPacked, whereFlagsPacked, pageIndex, watcherModel);

            if (watcherTitles.Count == 0)
                return NotFound("There are no watcher genres stored in the database");

            return Ok(watcherTitles);
        }

        [HttpGet("GetOneById/{watcher_name}/{genre_id}")]
        [Authorize(Policy = "BasicUserPolicy")]
        public async Task<IActionResult> GetWatcherGenreByIdAsync([FromHeader] string Authorization, int genre_id)
        {
            var watcherName = TokensManager.ExtractUserName(Authorization);

            var watcherModel = await watchersManager.GetOneByIdAsync(watcherName);
            if (watcherModel == null)
                return BadRequest("The watcher couldn not be found");

            var watcherGenre = await manager.GetOneByIdAsync(watcherName, genre_id);

            if (watcherGenre == null)
                return NotFound("There is no watcher genre with the given id stored in the database");

            return Ok(watcherGenre);
        }

        [HttpPost]
        [Authorize(Policy = "BasicUserPolicy")]
        public async Task<IActionResult> CreateWatcherGenreAsync([FromHeader] string Authorization, [FromBody] WatcherGenreModelParameter watcherGenreModelParam)
        {
            var watcherName = TokensManager.ExtractUserName(Authorization);

            var watcherModel = await watchersManager.GetOneByIdAsync(watcherName);
            if (watcherModel == null)
                return BadRequest("The watcher couldn not be found");

            watcherGenreModelParam.watcher_name = watcherName;
            await manager.Create(watcherGenreModelParam);
            return Ok();
        }

        [HttpPut("AddToScore")]
        [Authorize(Policy = "BasicUserPolicy")]
        public async Task<IActionResult> AddToWatcherGenreScoreAsync([FromHeader] string Authorization, [FromBody] WatcherGenreModelParameter watcherGenreModelParam)
        {
            var watcherName = TokensManager.ExtractUserName(Authorization);

            var watcherModel = await watchersManager.GetOneByIdAsync(watcherName);
            if (watcherModel == null)
                return BadRequest("The watcher couldn not be found");

            watcherGenreModelParam.watcher_name = watcherName;
            await manager.AddToScore(watcherGenreModelParam);
            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = "BasicUserPolicy")]
        public async Task<IActionResult> UpdateWatcherGenreAsync([FromHeader] string Authorization, [FromBody] WatcherGenreModelParameter watcherGenreModelParam)
        {
            var watcherName = TokensManager.ExtractUserName(Authorization);

            var watcherModel = await watchersManager.GetOneByIdAsync(watcherName);
            if (watcherModel == null)
                return BadRequest("The watcher couldn not be found");

            watcherGenreModelParam.watcher_name = watcherName;
            await manager.Update(watcherGenreModelParam);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteWatcherGenreAsync([FromHeader] string Authorization, [FromBody] string watcher_name, int genre_id)
        {
            var watcherName = TokensManager.ExtractUserName(Authorization);

            var watcherModel = await watchersManager.GetOneByIdAsync(watcherName);
            if (watcherModel == null)
                return BadRequest("The watcher couldn not be found");

            await manager.Delete(watcher_name, genre_id);
            return Ok();
        }

    }
}
