using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Repositories.IRepositories;

namespace Movie4U.Repositories
{
    public class WatcherGenresRepository : GenericRepository<WatcherGenre, WatcherGenreModel>, IWatcherGenresRepository
    {

        /**<summary>
        * Constructor.
        * </summary>>*/
        public WatcherGenresRepository(Movie4UContext db) : base(db) { }

    }
}