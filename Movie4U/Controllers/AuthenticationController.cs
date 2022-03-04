using Microsoft.AspNetCore.Mvc;
using Movie4U.Managers;
using Movie4U.Models;
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

        [HttpPost("Signup")]
        public async Task<IActionResult> Signup([FromBody] RegisterModel registerModel)
        {
            if (registerModel == null  || NullChecker.hasNulls(registerModel))
                return BadRequest("Invalid client request");
            
            var result = await manager.Signup(registerModel);

            if(result)
                return Ok("Signup succedeed");

            return BadRequest("Failed to signup");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null || NullChecker.hasNulls(loginModel))
                return BadRequest("Invalid client request");

            var tokens = await manager.Login(loginModel);

            if (tokens != null)
                return Ok(tokens);

            return BadRequest("Failed to login");
        }
    }
}
