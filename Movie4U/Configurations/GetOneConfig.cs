using System;
using System.Linq.Expressions;

namespace Movie4U.Configurations
{
    public class GetOneConfig<TEntity>
    {
        public Expression<Func<TEntity, object>>[] filterPropertySelectors { get; set; }
        public object[] filterValuesToMatch { get; set; }
        public Expression<Func<TEntity, object>>[] includers { get; set; }
        public bool asSplitQuery { get; set; }


        public GetOneConfig(Expression<Func<TEntity, object>>[] filterPropertySelectors, object[] filterValuesToMatch, Expression<Func<TEntity, object>>[] includers = null, bool asSplitQuery = false)
        {
            this.filterPropertySelectors = filterPropertySelectors;
            this.filterValuesToMatch = filterValuesToMatch;
            this.includers = includers;
            this.asSplitQuery = asSplitQuery;
        }

    }
}
