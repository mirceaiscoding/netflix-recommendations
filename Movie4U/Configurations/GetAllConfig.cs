using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Movie4U.Configurations
{
    public class GetAllConfig<TEntity>
    {
        public int orderByFlagsPacked { get; set; }
        public int whereFlagsPacked { get; set; }
        public int? pageIndex { get; set; }
        public List<Func<IQueryable<TEntity>, IQueryable<TEntity>>> extraEntityFilters { get; set; }
        public Expression<Func<TEntity, object>>[] includers;
        public bool asSplitQuery { get; set; }


        public GetAllConfig(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1, List<Func<IQueryable<TEntity>, IQueryable<TEntity>>> extraEntityFilters = null, Expression<Func<TEntity, object>>[] includers = null, bool asSplitQuery = false)
        {
            this.orderByFlagsPacked = orderByFlagsPacked;
            this.whereFlagsPacked = whereFlagsPacked;
            this.pageIndex = pageIndex;
            this.extraEntityFilters = extraEntityFilters;
            this.includers = includers;
            this.asSplitQuery = asSplitQuery;
        }

    }
}
