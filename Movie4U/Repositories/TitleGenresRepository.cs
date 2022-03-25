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
    public class TitleGenresRepository: GenericRepository<TitleGenre, TitleGenreModel>, ITitleGenresRepository
    {
        /**<summary>
        * Constructor.
        * </summary>>*/
        public TitleGenresRepository(Movie4UContext db) : base(db) { }


        public async Task<List<TitleGenreModel>> GetAllAsync()
        {
            var titleGenres = await db.TitleGenres.ToListAsync();

            return CastUtility.ToModelsList<TitleGenre, TitleGenreModel>(titleGenres);
        }

        public async Task<List<TitleGenreModel>> GetAllByNetflixIdAsync(string netflixId)
        {
            var allDb = await GetAllDbAsync();
            var titleGenres = 
                allDb
                .Where(tg => tg.netflix_id == netflixId)
                .ToList();

            return CastUtility.ToModelsList<TitleGenre, TitleGenreModel>(titleGenres);
        }

        public async Task<TitleGenreModel> GetOneByIdAsync(int genre_id, string netflix_id)
        {
            var titleGenre = await 
                db
                .TitleGenres
                .FirstOrDefaultAsync(tg => tg.genre_id == genre_id && tg.netflix_id == netflix_id);

            if (titleGenre != null)
                return new TitleGenreModel(titleGenre);

            return null;
        }

    }
}
