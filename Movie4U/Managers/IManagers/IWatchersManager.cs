using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface IWatchersManager
    {
        Task<List<WatcherModel>> GetAllFromPageAsync(GetAllConfig<Watcher> config = null);

        Task<WatcherModel> GetOneByIdAsync(string name);
        
        Task Create(string watcherName, string UserId);

        Task<bool> UpdadeRefreshTokenAndExpTime(string watcherName, string refreshToken, DateTime refTokExpTime);

        Task<TokensModel> UpdateRefreshToken(WatcherModel watcher);

        Task<bool> UpdateWatcherCountryId(string watcherName, int? countryId);

        Task<bool> UpdateNextPageIndex(string watcherName, int? nextPageIndex = 1);

        Task<bool> Delete(string name);
    }
}
