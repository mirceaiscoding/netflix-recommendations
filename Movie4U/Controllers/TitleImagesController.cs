using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie4U.EntitiesModels.Models;
using Movie4U.Managers.IManagers;
using System.Threading.Tasks;

namespace Movie4U.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TitleImagesController : ControllerBase
    {
        private readonly ITitleImagesManager manager;

        public TitleImagesController(ITitleImagesManager manager)
        {
            this.manager = manager;
        }

        [HttpGet("GetAllTitleImages")]
        [Authorize(Policy = "BasicUserPolicy")]
        public async Task<IActionResult> GetAllTitleImagesAsync()
        {
            var titleImages = await manager.GetAllAsync();

            if (titleImages.Count == 0)
                return NotFound("There are no title images stored in the database");

            return Ok(titleImages);
        }

        [HttpGet("GetTitleImageById/{genre_id}")]
        [Authorize(Policy = "BasicUserPolicy")]
        public async Task<IActionResult> GetGenreById([FromRoute] string url)
        {
            TitleImageModel titleImage = await manager.GetOneByIdAsync(url);

            if (titleImage == null)
                return NotFound("There is no title image with the given id stored in the database");

            return Ok(titleImage);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateTitleImage([FromBody] TitleImageModel titleImageModel)
        {
            await manager.Create(titleImageModel);
            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateTitleImage([FromBody] TitleImageModel titleImageModel)
        {
            await manager.Update(titleImageModel);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteTitleImage([FromBody] string url)
        {
            await manager.Delete(url);
            return Ok();
        }

    }
}
