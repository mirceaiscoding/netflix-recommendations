using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie4U.EntitiesModels.Models;
using Movie4U.Managers.IManagers;
using System.Threading.Tasks;

namespace Movie4U.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TitleGenresController : ControllerBase
    {
        private readonly ITitleGenresManager manager;

        public TitleGenresController(ITitleGenresManager manager)
        {
            this.manager = manager;
        }

        [HttpGet("GetAllTitleGenresFromPage/{pageIndex}")]
        [Authorize(Policy = "BasicUserPolicy")]
        public async Task<IActionResult> GetAllTitleGenresFromPageAsync([FromHeader] int orderByFlagsPacked = 0, [FromHeader] int whereFlagsPacked = 0, [FromRoute] int? pageIndex = 1)
        {
            var titleGenres = await manager.GetAllFromPageAsync(orderByFlagsPacked, whereFlagsPacked, pageIndex);

            if (titleGenres.Count == 0)
                return NotFound("There are no title genres stored in the database");

            return Ok(titleGenres);
        }

        [HttpGet("GetTitleGenreById/{genre_id}/{netflix_id}")]
        [Authorize(Policy = "BasicUserPolicy")]
        public async Task<IActionResult> GetTitleGenreByIdAsync([FromRoute] int genre_id, string netflix_id)
        {
            var titleGenre = await manager.GetOneByIdAsync(genre_id, netflix_id);

            if (titleGenre == null)
                return NotFound("There is no title genre with the given id stored in the database");

            return Ok(titleGenre);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateTitleGenreAsync([FromBody] TitleGenreModel titleGenreModel)
        {
            await manager.Create(titleGenreModel);
            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateTitleGenreAsync([FromBody] TitleGenreModel titleGenreModel)
        {
            await manager.Update(titleGenreModel);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteTitleGenreAsync([FromBody] int genre_id, string netflix_id)
        {
            await manager.Delete(genre_id, netflix_id);
            return Ok();
        }

    }
}