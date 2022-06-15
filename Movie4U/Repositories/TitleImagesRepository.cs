using AutoMapper;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Repositories.IRepositories;

namespace Movie4U.Repositories
{
    public class TitleImagesRepository: GenericRepository<TitleImage, TitleImageModel>, ITitleImagesRepository
    {
        /**
         * <summary>
         * Constructor.
         * </summary>>
         */
        public TitleImagesRepository(Movie4UContext db, IMapper mapper) : base(db, mapper) { }

    }
}
