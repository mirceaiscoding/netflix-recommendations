using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie4U.EntitiesModels.Entities;
using Movie4U.Repositories.IRepositories;
using Movie4U.Utilities;
using Movie4U.EntitiesModels;
using System;
using EFCore.BulkExtensions;

namespace Movie4U.Repositories
{
    public class GenericRepository<TEntity, TModel> : IGenericRepository<TEntity, TModel>
        where TEntity : EntitiesModelsBase<TEntity, TModel>, new()
        where TModel : EntitiesModelsBase<TEntity, TModel>, new()
    {
        readonly static int pageSize = 10;


        /**<summary>
         * The context.
         * </summary>*/
        internal Movie4UContext db;
        internal DbSet<TEntity> entities;

        /**<summary>
         * Constructor.
         * </summary>*/
        public GenericRepository(Movie4UContext db)
        {
            this.db = db;
            entities = db.Set<TEntity>();
        }


        public virtual async Task<List<TModel>> GetAllFilteredAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1, List<Func<TEntity, bool>> extraFilters = null, Func<List<TModel>, Task> filler = null)
        {
            var modelsList =
                CastUtility.ToModelsList<TEntity, TModel>
                    (await GetAllDbFilteredAsync(
                        orderByFlagsPacked,
                        whereFlagsPacked,
                        pageIndex,
                        extraFilters,
                        true));
            
            if (filler != null)
                await filler(modelsList);
            return modelsList;
        }

        public virtual async Task<List<TEntity>> GetAllDbFilteredAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1, List<Func<TEntity, bool>> extraFilters = null, bool asNoTracking = false)
        {
            var filterList = await GetFilterList(whereFlagsPacked);

            IQueryable<TEntity> result = entities;
            if (asNoTracking)
                result = result.AsNoTracking();

            foreach (var filter in filterList)
                result = result.Where(entity => filter(entity));

             var resultList = await result.ToListAsync();

            if (extraFilters != null)
                foreach (var filter in extraFilters)
                    resultList = resultList
                        .Where(entity => filter(entity))
                        .ToList();

            return resultList;
        }

        public virtual async Task<List<TModel>> GetAllOrderedAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1, List<Func<TEntity, bool>> extraFilters = null, Func<List<TModel>, Task> filler = null)
        {
            var result = 
                CastUtility
                .ToModelsList<TEntity, TModel>(
                    await GetAllDbFilteredAsync(
                        orderByFlagsPacked,
                        whereFlagsPacked,
                        pageIndex,
                        extraFilters,
                        true));

            if (filler != null)
                await filler(result);

            var comparerList = await GetTModelComparerList(orderByFlagsPacked);
            if (comparerList.Count() == 0)
                return result;

            result.Sort(new ModelChainComparer<TModel>(comparerList));
            return result;
        }

        public virtual async Task<List<TEntity>> GetAllDbOrderedAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1, List<Func<TEntity, bool>> extraFilters = null, bool asNoTracking = false)
        {
            var result = await GetAllDbFilteredAsync(
                orderByFlagsPacked,
                whereFlagsPacked,
                pageIndex,
                extraFilters,
                asNoTracking);

            var comparerList = await GetTEntityComparerList(orderByFlagsPacked);
            if (comparerList.Count() == 0)
                return result;

            result.Sort(new EntityChainComparer<TEntity>(comparerList));
            return result;
        }

        public virtual async Task<List<TModel>> GetAllFromPageAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1, List<Func<TEntity, bool>> extraFilters = null, Func<List<TModel>, Task> filler = null)
        {
            return await PaginatedListFactory<TModel>
                .Create(
                    await GetAllOrderedAsync(
                        orderByFlagsPacked,
                        whereFlagsPacked,
                        pageIndex,
                        extraFilters,
                        filler),
                    (int)pageIndex, pageSize);
        }

        public virtual async Task<List<TEntity>> GetAllDbFromPageAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageIndex = 1, List<Func<TEntity, bool>> extraFilters = null)
        {
            return await PaginatedListFactory<TEntity>
                .Create(
                    await GetAllDbOrderedAsync(
                        orderByFlagsPacked,
                        whereFlagsPacked,
                        pageIndex,
                        extraFilters),
                    (int)pageIndex, pageSize);
        }

        public virtual async Task<TModel> GetOneByIdAsync(object id, Func<TModel, Task> filler = null)
        {
            var entity = await entities.FindAsync(id);
            var model = CastUtility.ToModel<TEntity, TModel>(entity);

            if (filler != null)
                await filler(model);
            return model;
        }

        public virtual async Task<TModel> GetOneByIdAsync(object id1, object id2, Func<TModel, Task> filler = null)
        {
            var entity = await entities.FindAsync(id1, id2);
            var model = CastUtility.ToModel<TEntity, TModel>(entity);

            if (filler != null)
                await filler(model);
            return model;
        }

        public virtual async Task<TEntity> GetOneDbByIdAsync(object id)
        {
            return await entities.FindAsync(id);
        }

        public virtual async Task<TEntity> GetOneDbByIdAsync(object id1, object id2)
        {
            return await entities.FindAsync(id1, id2);
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            await entities.AddAsync(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity[]> InsertMultipleAsync(TEntity[] entities)
        {
            await this.entities.AddRangeAsync(entities);
            await db.SaveChangesAsync();
            return entities;
        }

        public async Task<TEntity[]> InsertOrUpdateMultipleAsync(TEntity[] entities)
        {
            await db.BulkInsertOrUpdateAsync(entities);
            await db.SaveChangesAsync();
            return entities;
        }

        public virtual async Task UpdateAsync(TEntity entityToUpdate)
        {
            entities.Attach(entityToUpdate);
            db.Entry(entityToUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(object id)
        {
            var entityToDelete = await entities.FindAsync(id);
            await DeleteAsync(entityToDelete);
            await db.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entityToDelete)
        {
            if (db.Entry(entityToDelete).State == EntityState.Detached)
                entities.Attach(entityToDelete);
            entities.Remove(entityToDelete);
            await db.SaveChangesAsync();
        }


        protected static Task<List<Func<TEntity, bool>>> GetFilterList(int whereFlagsPacked)
        {
            var flagsUnpacked = FlagsUtility.GetFlagsUnpacked(whereFlagsPacked);
            var filterList = new List<Func<TEntity, bool>>();
            if (flagsUnpacked.Count > 0)
                foreach (int flag in flagsUnpacked)
                    filterList.Add(new TEntity().GetFilter(flag));
            return Task.FromResult(filterList);
        }

        protected static Task<List<Func<TEntity, TEntity, int>>> GetTEntityComparerList(int orderByFlagsPacked)
        {
            var flagsUnpacked = FlagsUtility.GetFlagsUnpacked(orderByFlagsPacked);
            var comparerList = new List<Func<TEntity, TEntity, int>>();
            if (flagsUnpacked.Count > 0)
                foreach (int flag in flagsUnpacked)
                    comparerList.Add(new TEntity().GetTEntityComparer(flag));
            return Task.FromResult(comparerList);
        }

        protected static Task<List<Func<TModel, TModel, int>>> GetTModelComparerList(int orderByFlagsPacked)
        {
            var flagsUnpacked = FlagsUtility.GetFlagsUnpacked(orderByFlagsPacked);
            var comparerList = new List<Func<TModel, TModel, int>>();
            if (flagsUnpacked.Count > 0)
                foreach (int flag in flagsUnpacked)
                    comparerList.Add(new TModel().GetTModelComparer(flag));
            return Task.FromResult(comparerList);
        }

    }
}
