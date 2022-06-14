using Microsoft.AspNetCore.Identity;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Managers.IManagers;
using Movie4U.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers
{
    public class WatchersManager: IWatchersManager
    {
        private readonly IWatchersRepository repo;
        private readonly UserManager<User> userManager;
        private readonly ITokensManager tokensManager;

        /**<summary>
         * Constructor.
         * </summary>*/
        public WatchersManager(IWatchersRepository repo, UserManager<User> userManager, ITokensManager tokensManager)
        {
            this.repo = repo;
            this.userManager = userManager;
            this.tokensManager = tokensManager;
        }


        public async Task<List<WatcherModel>> GetAllFromPageAsync(GetAllConfig<Watcher> config = null)
        {
            return await repo.GetAllFromPageAsync(config);
        }

        public async Task<WatcherModel> GetOneByIdAsync(string name)
        {
            return await repo.GetOneByIdAsync(name);
        }

        public async Task Create(string watcherName, string UserId)
        {
            var newWatcher = new Watcher
            {
                watcher_name = watcherName,
                register_date = DateTime.Now,
                userId = UserId
            };

            await repo.InsertAsync(newWatcher);
        }

        public async Task<bool> UpdadeRefreshTokenAndExpTime(string watcherName, string refreshToken, DateTime refTokExpTime)
        {
            var watcher = await repo.GetOneDbByIdAsync(watcherName);
            if (watcher == null)
                return false;
            
            watcher.refreshToken = refreshToken;
            watcher.refreshTokenExpiryTime = refTokExpTime;

            return await repo.UpdateAsync(watcher);
        }

        public async Task<TokensModel> UpdateRefreshToken(WatcherModel watcher)
        {
            User user = await userManager.FindByNameAsync(watcher.watcher_name);
            if (user == null)
                return null;

            var accessToken = tokensManager.GenerateAccessToken(user).Result;
            var refreshToken = tokensManager.GenerateRefreshToken();

            var dbWatcher = await repo.GetOneDbByIdAsync(watcher.watcher_name);
            if (dbWatcher == null)
                return null;

            dbWatcher.refreshToken = refreshToken;
            var result = await repo.UpdateAsync(dbWatcher);
            if (!result)
                return null;

            var tokensModel = new TokensModel
            {
                accessToken = accessToken,
                refreshToken = refreshToken
            };
            return tokensModel;
        }

        public async Task<bool> UpdateWatcherCountryId(string watcherName, int? countryId)
        {
            var dbWatcher = await repo.GetOneDbByIdAsync(watcherName);
            if(dbWatcher == null)
                return false;

            dbWatcher.coutryId = countryId;

            await repo.UpdateAsync(dbWatcher);
            return true;
        }

        public async Task<bool> UpdateNextPageIndex(string watcherName, int? nextPageIndex = 1)
        {
            var dbWatcher = await repo.GetOneDbByIdAsync(watcherName);
            if (dbWatcher == null)
                return false;

            dbWatcher.nextPageIndex = (int)nextPageIndex;

            await repo.UpdateAsync(dbWatcher);
            return true;
        }

        public async Task<bool> Delete(string name)
        {
            var delWatcher = await repo.GetOneDbByIdAsync(name);
            if (delWatcher == null)
                return false;

            return await repo.DeleteAsync(delWatcher);
        }

    }
}
