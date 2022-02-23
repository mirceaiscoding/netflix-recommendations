using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Movie4U.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Movie4U.Managers
{
    public class TokensManager : ITokensManager
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<User> userManager;

        public TokensManager(IConfiguration configuration, UserManager<User> userManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
        }

        public async Task<string> GenerateToken(User user)
        {
            var roles = await userManager.GetRolesAsync(user);
            var claims = new List<Claim>();

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserName));

            var secretKey =
                configuration
                .GetSection("Jwt")
                .GetSection("SecretKey")
                .Get<string>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                //Expires = DateTime.Now.AddHours(2),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }

        public async Task<string> ExtractUserName(string tokenHeader)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenHeader.Split(' ')[1]); // token format is:    Bearer + space + token
            var claims = token.Payload.Claims.ToList();

            var watcherName = "";
            foreach (var claim in claims)
                if (claim.Type == "nameid")
                    watcherName = claim.Value;

            return watcherName;
        }
    }
}
