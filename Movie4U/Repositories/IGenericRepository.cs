﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetQueryable();

        IEnumerable<TEntity> GetAllDb();
        Task<List<TEntity>> GetAllDbAsync();

        TEntity GetByID(object id);
        Task<TEntity> GetByIdAsync(object id);

        void Insert(TEntity entity);
        Task<TEntity> InsertAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(object id);

        Task UpdateAsync(TEntity entity);
    }
}
