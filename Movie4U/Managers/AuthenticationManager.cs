using Microsoft.AspNetCore.Identity;
using Movie4U.Entities;
using Movie4U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Managers
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ITokensManager tokenManager;

        private readonly IWatchersManager watchersManager;

        public AuthenticationManager(UserManager<User> userManager, SignInManager<User> signInManager, ITokensManager tokenManager, IWatchersManager watchersManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenManager = tokenManager;

            this.watchersManager = watchersManager;
        }

        public async Task Signup(RegisterModel registerModel)
        {
            User user = new User
            {
                Email = registerModel.email,
                UserName = registerModel.userName
            };

            var result = await userManager.CreateAsync(user, registerModel.password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, registerModel.role);
                await watchersManager.Create(user.UserName, user.Id);
            }
        }

        public async Task<TokensModel> Login(LoginModel loginModel)
        {
            var user = await userManager.FindByEmailAsync(loginModel.email);
            if (user != null)
            {
                var result = await signInManager.CheckPasswordSignInAsync(user, loginModel.password, false);
                if (result.Succeeded)
                {
                    var token = await tokenManager.GenerateToken(user);

                    return new TokensModel
                    {
                        accessToken = token
                    };
                }
            }

            return null;
        }
    }
}
