using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movie4U.Entities;
using Movie4U.Models;
using Movie4U.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Managers
{
    public class WatchersManager: IWatchersManager
    {
        private readonly IWatchersRepository repo;
        private readonly UserManager<User> userManager;
        private readonly ITokensManager tokensManager;

        public WatchersManager(IWatchersRepository repo, UserManager<User> userManager, ITokensManager tokensManager)
        {
            this.repo = repo;
            this.userManager = userManager;
            this.tokensManager = tokensManager;
        }

        public async Task<List<WatcherModel>> GetAllAsync()
        {
            return await repo.GetAllAsync();
        }


        public async Task Create(string watcherName, string UserId)
        {
            Watcher newWatcher = new Watcher
            {
                watcher_name = watcherName,
                register_date = DateTime.Now,
                userId = UserId
            };

            await repo.InsertAsync(newWatcher);
        }

        public async Task UpdadeRefreshTokenAndExpTime(string watcherName, string refreshToken, DateTime refTokExpTime)
        {
            Watcher watcher = await repo.GetDbWatcherAsync(watcherName);
            
            watcher.refreshToken = refreshToken;
            watcher.refreshTokenExpiryTime = refTokExpTime;

            await repo.UpdateAsync(watcher);
        }

        public async Task<TokensModel> UpdateRefreshToken(WatcherModel watcher)
        {
            User user =
                await
                userManager
                .FindByNameAsync(watcher.watcher_name);
            
            var accessToken =
                tokensManager
                .GenerateAccessToken(user)
                .Result;
            var refreshToken =
                tokensManager
                .GenerateRefreshToken();

            Watcher dbWatcher = await repo.GetDbWatcherAsync(watcher.watcher_name);
            dbWatcher.refreshToken = refreshToken;

            await repo.UpdateAsync(dbWatcher);

            TokensModel tokensModel = new TokensModel
            {
                accessToken = accessToken,
                refreshToken = refreshToken
            };
            return tokensModel;
        }

        public async Task Delete(string name)
        {
            Watcher watcher = await repo.GetDbWatcherAsync(name);

            if (watcher != null)
                await repo.DeleteAsync(watcher);
        }

        public async Task<WatcherModel> GetWatcherAsync(string name)
        {
            return await repo.GetWatcherAsync(name);
        }
    }
}
