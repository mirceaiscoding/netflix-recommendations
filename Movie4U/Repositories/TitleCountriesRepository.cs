using AutoMapper;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Repositories.IRepositories;

namespace Movie4U.Repositories
{
    public class TitleCountriesRepository: GenericRepository<TitleCountry, TitleCountryModel>, ITitleCountriesRepository
    {

        /**<summary>
        * Constructor.
        * </summary>>*/
        public TitleCountriesRepository(Movie4UContext db, IMapper mapper) : base(db, mapper) { }

    }
}
