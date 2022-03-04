using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movie4U.Entities;
using Movie4U.Managers;
using Movie4U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly ITokensManager manager;
        private readonly IWatchersManager watchersManager;

        public TokensController(ITokensManager manager, IWatchersManager watchersManager)
        {
            this.manager = manager;
            this.watchersManager = watchersManager;
        }

        [HttpPost("Refresh")]
        public IActionResult Refresh(TokensModel tokensModel)
        {
            if (tokensModel == null || NullChecker.hasNulls(tokensModel))
            {
                return BadRequest("Invalid client request");
            }

            string accessToken = tokensModel.accessToken;
            string refreshToken = tokensModel.refreshToken;

            var principal = manager.GetPrincipalFromExpiredToken(accessToken);
            var userName = principal.Identity.Name;      // mapped to the Name claim by default

            WatcherModel watcher = watchersManager.GetWatcher(userName);
            if (watcher == null || watcher.refreshToken != refreshToken || watcher.refreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid client request");

            // generate new tokens and update watcher in the database
            tokensModel.copy(
                watchersManager
                .UpdateRefreshToken(watcher)
                .Result);

            return Ok(tokensModel);
        }

        [HttpPost("Revoke")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Revoke([FromHeader] string Authorization)
        {
            var watcherName = await manager.ExtractUserName(Authorization);

            var dbWatcher = watchersManager.GetWatcher(watcherName);
            if (dbWatcher == null)
                return BadRequest("The watcher couldn not be found");

            await watchersManager.UpdadeRefreshTokenAndExpTime(watcherName, null, DateTime.Now);

            return NoContent();
        }

    }
}
