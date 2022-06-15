using AutoMapper;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Repositories.IRepositories;

namespace Movie4U.Repositories
{
    public class WatcherTitlesRepository: GenericRepository<WatcherTitle, WatcherTitleModel>, IWatcherTitlesRepository
    {
        /**<summary>
        * Constructor.
        * </summary>>*/
        public WatcherTitlesRepository(Movie4UContext db, IMapper mapper) : base(db, mapper) { }
        
    }
}
