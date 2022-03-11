using Microsoft.EntityFrameworkCore;
using Movie4U.Entities;
using Movie4U.Models;
using Movie4U.Repositories.IRepositories;
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
        public GenresRepository(Movie4UContext db) : base(db)
        {
            this.db = db;
        }

        public async Task<List<GenreModel>> GetAllAsync()
        {
            var genres = await db.Genres.ToListAsync();

            var genreModels = new List<GenreModel>();
            foreach (var genre in genres)
            {
                var genreModel = new GenreModel();
                genreModel.Copy(genre);

                genreModels.Add(genreModel);
            }

            return genreModels;
        }

        public async Task<GenreModel> GetOneByIdAsync(int genre_id)
        {
            var genre = await GetOneDbByIdAsync(genre_id);

            if (genre != null)
            {
                var genreModel = new GenreModel();
                genreModel.Copy(genre);

                return genreModel;
            }

            return null;
        }
    }
}
