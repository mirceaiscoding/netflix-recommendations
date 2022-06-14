using System;
using System.Collections.Generic;
using System.Linq;

namespace Movie4U.EntitiesModels.Models
{
    public class GetAllConfig<TEntity>
    {
        public int orderByFlagsPacked { get; set; }
        public int whereFlagsPacked { get; set; }
        public int? pageIndex { get; set; }
        public List<Func<IQueryable<TEntity>, IQueryable<TEntity>>> extraEntityFilters { get; set; }


        public GetAllConfig(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1, List<Func<IQueryable<TEntity>, IQueryable<TEntity>>> extraEntityFilters = null)
        {
            this.orderByFlagsPacked = orderByFlagsPacked;
            this.whereFlagsPacked = whereFlagsPacked;
            this.pageIndex = pageIndex;
            this.extraEntityFilters = extraEntityFilters;
        }

    }
}
