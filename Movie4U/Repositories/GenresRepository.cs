using Microsoft.EntityFrameworkCore;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Repositories.IRepositories;
using Movie4U.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Repositories
{
    public class GenresRepository : GenericRepository<Genre>, IGenresRepository
    {
        /**<summary>
         * Constructor.
         * </summary>*/
        public GenresRepository(Movie4UContext db) : base(db) { }


        public async Task<List<GenreModel>> GetAllAsync()
        {
            var genres = await db.Genres.ToListAsync();

            return CastUtility.ToModels<Genre, GenreModel>(genres);
        }

        public async Task<GenreModel> GetOneByIdAsync(int genre_id)
        {
            var genre = await GetOneDbByIdAsync(genre_id);

            if (genre != null)
                return new GenreModel(genre);

            return null;
        }
    }
}
