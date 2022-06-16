using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Repositories.IRepositories;
using System;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface IWatchersManager: IGenericManager<Watcher, WatcherModel, IWatchersRepository>
    {
        Task Create(string watcherName, string UserId);

        Task<bool> UpdadeRefreshTokenAndExpTime(string watcherName, string refreshToken, DateTime refTokExpTime);

        Task<TokensModel> UpdateRefreshToken(WatcherModel watcher);

        Task<bool> UpdateWatcherCountryId(string watcherName, int? countryId);

        Task<bool> UpdateNextPageIndex(string watcherName, int? nextPageIndex = 1);
    }
}
