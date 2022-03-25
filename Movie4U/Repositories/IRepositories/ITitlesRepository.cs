using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Repositories.IRepositories
{
    public interface ITitlesRepository: IGenericRepository<Title, TitleModel>
    {
        Task<List<TitleModel>> GetAllAsync();

        Task<TitleModel> GetOneByIdAsync(string netflix_id);
    }
}
