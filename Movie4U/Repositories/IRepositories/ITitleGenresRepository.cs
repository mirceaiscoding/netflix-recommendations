using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Repositories.IRepositories
{
    public interface ITitleGenresRepository: IGenericRepository<TitleGenre, TitleGenreModel>
    {
        Task<List<TitleGenreModel>> GetAllAsync();

        Task<List<TitleGenreModel>> GetAllByNetflixIdAsync(string netflixId);

        Task<TitleGenreModel> GetOneByIdAsync(int genre_id, string netflix_id);
    }
}
