using Movie4U.Configurations;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Repositories.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface ITitlesManager: IGenericManager<Title, TitleModel, ITitlesRepository>
    {
        new Task<List<TitleModel>> GetAllFromPageAsync(GetAllConfig<Title> config = null);

        Task<TitleModel> GetOneByIdAsync(int netflix_id);

        Task Create(TitleModelParameter titleModelParam);

        Task CreateMultiple(TitleModel[] models);

        Task CreateOrUpdateMultiple(TitleModel[] models);

        Task<bool> Update(TitleModelParameter titleModelParam);
    }
}
