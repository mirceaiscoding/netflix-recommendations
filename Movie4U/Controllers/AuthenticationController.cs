using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie4U.Managers;
using Movie4U.Models;
using Movie4U.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationManager manager;

        public AuthenticationController(IAuthenticationManager manager)
        {
            this.manager = manager;
        }

        [HttpPost("SignupUser")]
        public async Task<IActionResult> SignupUser([FromBody] RegisterModel registerModel)
        {
            registerModel.role = "BasicUser";

            if (registerModel == null  || NullChecker.hasNulls(registerModel))
                return BadRequest("Invalid client request");
            
            var result = await manager.Signup(registerModel);

            if(result)
                return Ok("Signup succedeed");

            return BadRequest("Failed to signup");
        }

        [HttpPost("Signup")]
        // TODO: Add this in production: [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Signup([FromBody] RegisterModel registerModel)
        {
            if (registerModel == null  || NullCheckerUtility.hasNulls(registerModel))
                return BadRequest("Invalid client request");

            var result = await manager.Signup(registerModel);

            if (result)
                return Ok("Signup succedeed");

            return BadRequest("Failed to signup");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null || NullCheckerUtility.hasNulls(loginModel))
                return BadRequest("Invalid client request");

            var tokens = await manager.Login(loginModel);

            if (tokens != null)
                return Ok(tokens);

            return BadRequest("Failed to login");
        }
    }
}
