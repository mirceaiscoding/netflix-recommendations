using Movie4U.Configurations;
using Movie4U.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Movie4U.Utilities
{
    public static class NetflixIdDependentsUtility<TEntity>
    {
        /// <summary>
        /// Adds the netflixId extraEntityFilter to config.
        /// </summary>
        /// <param name="netflixId">The neflixId to filter by.</param>
        /// <param name="config">The GetAllConfig. If null,  it is instantiated with the netflixId filter.</param>
        /// <returns>The new resulting configuration.</returns>
        public static GetAllConfig<TEntity> AddNetflixIdExtraEntityFilter(int netflixId, GetAllConfig<TEntity> config = null)
        {
            if (config == null)
                config = new GetAllConfig<TEntity>();

            if (config.extraEntityFilters == null)
            {
                config.extraEntityFilters = new List<Func<IQueryable<TEntity>, IQueryable<TEntity>>>()
                {
                    source => source.PropertyFilter("netflix_id", netflixId)
                };

                return config;
            }
            
            config.extraEntityFilters.Add(source => source.PropertyFilter("netflix_id", netflixId));

            return config;
        }
    }
}
