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

        [HttpGet("GetAllFromPage/{pageIndex}")]
        [Authorize(Policy = "BasicUserPolicy")]
        public async Task<IActionResult> GetAllTitleImagesFromPageAsync([FromHeader] int orderByFlagsPacked = 0, [FromHeader] int whereFlagsPacked = 0, [FromRoute] int? pageIndex = 1)
        {
            var titleImages = await manager.GetAllFromPageAsync(orderByFlagsPacked, whereFlagsPacked, pageIndex);

            if (titleImages.Count == 0)
                return NotFound("There are no title images stored in the database");

            return Ok(titleImages);
        }

        [HttpGet("GetOneById/{imgurl}")]
        [Authorize(Policy = "BasicUserPolicy")]
        public async Task<IActionResult> GetTitleImageByIdAsync([FromRoute] string imgurl)
        {
            var titleImage = await manager.GetOneByIdAsync(imgurl);

            if (titleImage == null)
                return NotFound("There is no title image with the given id stored in the database");

            return Ok(titleImage);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateTitleImageAsync([FromBody] TitleImageModel titleImageModel)
        {
            await manager.Create(titleImageModel);
            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateTitleImageAsync([FromBody] TitleImageModel titleImageModel)
        {
            await manager.Update(titleImageModel);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteTitleImageAsync([FromBody] string url)
        {
            await manager.Delete(url);
            return Ok();
        }

    }
}
