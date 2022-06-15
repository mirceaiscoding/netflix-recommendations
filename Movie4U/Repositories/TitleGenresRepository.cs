using AutoMapper;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Repositories.IRepositories;

namespace Movie4U.Repositories
{
    public class TitleGenresRepository: GenericRepository<TitleGenre, TitleGenreModel>, ITitleGenresRepository
    {
        /**<summary>
        * Constructor.
        * </summary>>*/
        public TitleGenresRepository(Movie4UContext db, IMapper mapper) : base(db, mapper) { }

    }
}
