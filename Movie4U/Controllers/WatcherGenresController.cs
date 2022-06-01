using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie4U.EntitiesModels.Models;
using Movie4U.Enums;
using Movie4U.Managers.IManagers;
using System.Threading.Tasks;

namespace Movie4U.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WatcherGenresController : ControllerBase
    {
        private readonly IWatcherGenresManager manager;

        public WatcherGenresController(IWatcherGenresManager manager)
        {
            this.manager = manager;
        }

        [HttpGet("GetAllWatcherGenres")]
        [Authorize(Policy = "BasicUserPolicy")]
        public async Task<IActionResult> GetAllWatcherGenresAsync([FromRoute] int orderByFlagsPacked = 0, [FromRoute] int whereFlagsPacked = 0, [FromRoute] int? pageNumber = 1)
        {
            var watcherTitles = await manager.GetAllAsync(orderByFlagsPacked, whereFlagsPacked, pageNumber);

            if (watcherTitles.Count == 0)
                return NotFound("There are no watcher genres stored in the database");

            return Ok(watcherTitles);
        }

        [HttpGet("GetWatcherGenreById/{watcher_name}/{genre_id}")]
        [Authorize(Policy = "BasicUserPolicy")]
        public async Task<IActionResult> GetWatcherGenreByIdAsync([FromRoute] string watcher_name, int genre_id)
        {
            var watcherGenre = await manager.GetOneByIdAsync(watcher_name, genre_id);

            if (watcherGenre == null)
                return NotFound("There is no watcher genre with the given id stored in the database");

            return Ok(watcherGenre);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateWatcherGenreAsync([FromBody] WatcherGenreModelParameter watcherGenreModelParam)
        {
            await manager.Create(watcherGenreModelParam);
            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateWatcherGenreAsync([FromBody] WatcherGenreModelParameter watcherGenreModelParam)
        {
            await manager.Update(watcherGenreModelParam);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteWatcherGenreAsync([FromBody] string watcher_name, int genre_id)
        {
            await manager.Delete(watcher_name, genre_id);
            return Ok();
        }

    }
}
