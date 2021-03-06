using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie4U.EntitiesModels.Models;
using Movie4U.Managers;
using Movie4U.Managers.IManagers;
using Movie4U.Utilities;
using System;
using System.Threading.Tasks;

namespace Movie4U.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly ITokensManager manager;
        private readonly IWatchersManager watchersManager;

        /**<summary>
         * Constructor.
         * </summary>*/
        public TokensController(ITokensManager manager, IWatchersManager watchersManager)
        {
            this.manager = manager;
            this.watchersManager = watchersManager;
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> RefreshAsync(TokensModel tokensModel)
        {
            if (tokensModel == null || NullCheckerUtility.HasNulls(tokensModel))
                return BadRequest("Invalid client request");

            string accessToken = tokensModel.accessToken;
            string refreshToken = tokensModel.refreshToken;

            var userName = 
                manager
                .GetPrincipalFromExpiredToken(accessToken)
                .Identity
                .Name;      // mapped to the Name claim by default

            var watcherModel = await watchersManager.GetOneByIdAsync(userName);
            if (watcherModel == null || watcherModel.refreshToken != refreshToken || watcherModel.refreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid client request");

            // generate new tokens and update watcher in the database
            tokensModel.Copy(
                watchersManager
                .UpdateRefreshToken(watcherModel)
                .Result);

            return Ok(tokensModel);
        }

        [HttpPost("Revoke")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Revoke([FromHeader] string Authorization)
        {
            var watcherName = TokensManager.ExtractUserName(Authorization);

            var watcherModel = await watchersManager.GetOneByIdAsync(watcherName);
            if (watcherModel == null)
                return BadRequest("The watcher couldn not be found");

            await watchersManager.UpdadeRefreshTokenAndExpTime(watcherName, null, DateTime.Now);

            return NoContent();
        }

    }
}
