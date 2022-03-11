using Movie4U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Managers.IManagers
{
    public interface IGenresManager
    {
        Task<List<GenreModel>> GetAllAsync();

        Task<GenreModel> GetOneByIdAsync(int genre_id);

        Task Update(GenreModel genreModel);

        Task Create(GenreModel genreModel);

        Task Delete(int genre_id);
    }
}
