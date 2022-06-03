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
    public class TitleCountriesRepository: GenericRepository<TitleCountry, TitleCountryModel>, ITitleCountriesRepository
    {

        /**<summary>
        * Constructor.
        * </summary>>*/
        public TitleCountriesRepository(Movie4UContext db) : base(db) { }

    }
}
