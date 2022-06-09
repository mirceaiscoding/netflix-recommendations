using Movie4U.EntitiesModels.Models;
using Movie4U.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface ITitlesManager
    {
        Task<List<TitleModel>> GetAllFromPageAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1);

        Task<TitleModel> GetOneByIdAsync(int netflix_id);

        Task Create(TitleModelParameter titleModelParam);

        Task CreateMultiple(TitleModel[] models);

        Task CreateOrUpdateMultiple(TitleModel[] models);

        Task Update(TitleModelParameter titleModelParam);

        Task Delete(int netflix_id);
    }
}
