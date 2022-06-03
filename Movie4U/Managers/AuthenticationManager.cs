using Microsoft.AspNetCore.Identity;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Managers.IManagers;
using System;
using System.Threading.Tasks;

namespace Movie4U.Managers
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ITokensManager tokensManager;
        private readonly IWatchersManager watchersManager;

        public AuthenticationManager(UserManager<User> userManager, SignInManager<User> signInManager, ITokensManager tokensManager, IWatchersManager watchersManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokensManager = tokensManager;

            this.watchersManager = watchersManager;
        }

        public async Task<bool> Signup(RegisterModel registerModel)
        {
            User user = new User
            {
                Email = registerModel.email,
                UserName = registerModel.userName
            };

            var result = await userManager.CreateAsync(user, registerModel.password);

            if (!result.Succeeded)
                return false;

            try
            {
                await userManager.AddToRoleAsync(user, registerModel.role);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                try
                {
                    await userManager.AddToRoleAsync(user, "BasicUser");
                }
                catch (Exception e2)
                {
                    Console.WriteLine(e2);
                    return false;
                }
            }

            await watchersManager.Create(user.UserName, user.Id);

            return true;
        }


        public async Task<TokensModel> Login(LoginModel loginModel)
        {
            User user;
            if(loginModel.emailOrName.Contains("@"))
                user = await userManager.FindByEmailAsync(loginModel.emailOrName);
            else
                user = await userManager.FindByNameAsync(loginModel.emailOrName);

            if (user == null)
                return null;

            var result = await signInManager.CheckPasswordSignInAsync(user, loginModel.password, false);
            if (!result.Succeeded)
                return null;

            var accessToken = await tokensManager.GenerateAccessToken(user);
            var refreshToken = tokensManager.GenerateRefreshToken();
            DateTime refTokExpTime = DateTime.Now.AddDays(7);

            await watchersManager.UpdadeRefreshTokenAndExpTime(user.UserName, refreshToken, refTokExpTime);

            return new TokensModel
            {
                accessToken = accessToken,
                refreshToken = refreshToken
            };
        }
    }
}
