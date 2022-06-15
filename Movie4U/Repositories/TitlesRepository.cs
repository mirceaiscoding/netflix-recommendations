using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Repositories
{
    public class TitlesRepository : GenericRepository<Title, TitleModel>, ITitlesRepository
    {
        /**<summary>
         * Constructor.
         * </summary>*/
        public TitlesRepository(Movie4UContext db, IMapper mapper) : base(db, mapper) { }

    }
}
