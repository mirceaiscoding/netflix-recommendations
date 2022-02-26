using Microsoft.AspNetCore.Mvc;
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
            var response = await manager.Signup(registerModel);

            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var tokens = await manager.Login(loginModel);

            if (tokens != null)
                return Ok(tokens);

            return BadRequest("Failed to login");
        }
    }
}
