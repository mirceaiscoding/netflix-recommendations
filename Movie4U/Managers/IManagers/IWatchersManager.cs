using Movie4U.EntitiesModels.Models;
using Movie4U.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface IWatchersManager
    {
        Task<List<WatcherModel>> GetAllFromPageAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1);

        Task<WatcherModel> GetOneByIdAsync(string name);
        
        Task Create(string watcherName, string UserId);

        Task UpdadeRefreshTokenAndExpTime(string watcherName, string refreshToken, DateTime refTokExpTime);

        Task<TokensModel> UpdateRefreshToken(WatcherModel watcher);

        Task<bool> UpdateWatcherCountryId(string watcherName, int? countryId);

        Task Delete(string name);
    }
}
