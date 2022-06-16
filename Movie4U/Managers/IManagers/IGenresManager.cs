using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.EntitiesModels.Models.uNoGS;
using Movie4U.Repositories.IRepositories;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface IGenresManager: IGenericManager<Genre, GenreModel, IGenresRepository>
    {
        Task CreateMultiple(GenreResponseModel[] models);
    }
}
