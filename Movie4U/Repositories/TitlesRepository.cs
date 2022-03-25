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
    public class TitlesRepository : GenericRepository<Title, TitleModel>, ITitlesRepository
    {
        /**<summary>
         * Constructor.
         * </summary>*/
        public TitlesRepository(Movie4UContext db) : base(db) { }


        public async Task<List<TitleModel>> GetAllAsync()
        {
            var titles = await db.Titles.ToListAsync();

            return CastUtility.ToModelsList<Title, TitleModel>(titles);
        }

        public async Task<TitleModel> GetOneByIdAsync(string netflix_id)
        {
            var title = await GetOneDbByIdAsync(netflix_id);

            if (title != null)
                return new TitleModel(title);

            return null;
        }
    }
}
