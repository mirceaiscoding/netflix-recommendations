using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Repositories.IRepositories
{
    public interface IGenresRepository: IGenericRepository<Genre, GenreModel>
    {
        Task<List<GenreModel>> GetAllAsync();

        Task<GenreModel> GetOneByIdAsync(int genre_id);
    }
}
