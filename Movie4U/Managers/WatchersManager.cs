using Microsoft.AspNetCore.Identity;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Managers.IManagers;
using Movie4U.Repositories.IRepositories;
using Movie4U.Utilities;
using System;
using System.Threading.Tasks;

namespace Movie4U.Managers
{
    public class WatchersManager: GenericManager<Watcher, WatcherModel, IWatchersRepository>, IWatchersManager
    {
        private readonly UserManager<User> userManager;
        private readonly ITokensManager tokensManager;

        /**<summary>
         * Constructor.
         * </summary>*/
        public WatchersManager(IWatchersRepository repo, UserManager<User> userManager, ITokensManager tokensManager): base(repo)
        {
            this.userManager = userManager;
            this.tokensManager = tokensManager;
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
            var watcher = await repo
                .GetOneDbByIdAsync(
                    GetOneConfigFactory<Watcher, WatcherModel>.New(
                        new object[] { watcherName }));

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

            var dbWatcher = await repo
                .GetOneDbByIdAsync(
                    GetOneConfigFactory<Watcher, WatcherModel>.New(
                        new object[] { watcher.watcher_name })); 

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
            var dbWatcher = await repo
                .GetOneDbByIdAsync(
                    GetOneConfigFactory<Watcher, WatcherModel>.New(
                        new object[] { watcherName }));

            if(dbWatcher == null)
                return false;

            dbWatcher.coutryId = countryId;

            await repo.UpdateAsync(dbWatcher);
            return true;
        }

        public async Task<bool> UpdateNextPageIndex(string watcherName, int? nextPageIndex = 1)
        {
            var dbWatcher = await repo
                .GetOneDbByIdAsync(
                    GetOneConfigFactory<Watcher, WatcherModel>.New(
                        new object[] { watcherName }));

            if (dbWatcher == null)
                return false;

            dbWatcher.nextPageIndex = (int)nextPageIndex;

            await repo.UpdateAsync(dbWatcher);
            return true;
        }

    }
}
