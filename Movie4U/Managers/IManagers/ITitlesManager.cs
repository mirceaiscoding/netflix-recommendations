using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface ITitlesManager
    {
        Task<List<TitleModel>> GetAllFromPageAsync(GetAllConfig<Title> config = null);

        Task<TitleModel> GetOneByIdAsync(int netflix_id);

        Task Create(TitleModelParameter titleModelParam);

        Task CreateMultiple(TitleModel[] models);

        Task CreateOrUpdateMultiple(TitleModel[] models);

        Task<bool> Update(TitleModelParameter titleModelParam);

        Task<bool> Delete(int netflix_id);
    }
}
