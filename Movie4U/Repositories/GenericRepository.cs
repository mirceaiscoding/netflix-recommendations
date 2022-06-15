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
using Movie4U.EntitiesModels.Models;
using AutoMapper;
using Movie4U.ExtensionMethods;

namespace Movie4U.Repositories
{
    public class GenericRepository<TEntity, TModel> : IGenericRepository<TEntity, TModel>
        where TEntity : EntitiesModelsBase<TEntity, TModel>, new()
        where TModel : EntitiesModelsBase<TEntity, TModel>, new()
    {
        readonly static int pageSize = 10;
        protected Action<IMappingOperationOptions<IQueryable<TEntity>, List<TModel>>> optsAll;
        protected Action<IMappingOperationOptions<TEntity, TModel>> optsOne;

        protected readonly IMapper mapper;

        /**<summary>
         * The context.
         * </summary>*/
        internal Movie4UContext db;
        internal DbSet<TEntity> entities;

        /**<summary>
         * Constructor.
         * </summary>*/
        public GenericRepository(Movie4UContext db, IMapper mapper)
        {
            this.db = db;
            entities = db.Set<TEntity>();
            this.mapper = mapper;

            optsAll = opt =>
            {
                opt.Items["Countries"] = db.Countries;
                opt.Items["Genres"] = db.Genres;
                opt.Items["WatcherGenres"] = db.WatcherGenres;
            };
            optsOne = opt =>
            {
                opt.Items["Countries"] = db.Countries;
                opt.Items["Genres"] = db.Genres;
                opt.Items["WatcherGenres"] = db.WatcherGenres;
            };
        }


        public virtual async Task<IQueryable<TEntity>> GetAllDbFilteredAsync(GetAllConfig<TEntity> config = null, bool asNoTracking = false)
        {
            IQueryable<TEntity> result = entities;
            if (asNoTracking)
                result = result.AsNoTracking();

            if (config == null)
                return result;

            var filterList = await GetDynamicEntityFilterList(config.whereFlagsPacked);
            foreach (var filter in filterList)
                result = filter(result);

            if (config.extraEntityFilters == null)
                return result;

            foreach (var filter in config.extraEntityFilters)
                result = filter(result);

            if(config.includers == null || config.includers.Count() == 0)
                return result;

            return result
                .IncludeMultiple(config.includers);
        }

        public virtual async Task<List<TModel>> GetAllOrderedAsync(GetAllConfig<TEntity> config = null, List<Func<TModel, bool>> extraModelFilters = null, Func<List<TModel>, Task> filler = null)
        {
            var result = mapper.Map(
                await GetAllDbFilteredAsync(config, true),
                optsAll);

/*            if (filler != null)
                await filler(result);*/

            if (extraModelFilters != null)
                foreach (var filter in extraModelFilters)
                    result = result
                        .Where(model => filter(model))
                        .ToList();

            if (config == null)
                return result;

            var comparerList = await GetTModelComparerList(config.orderByFlagsPacked);
            if (comparerList.Count() == 0)
                return result;

            result.Sort(new ModelChainComparer<TModel>(comparerList));
            return result;
        }

        public virtual async Task<List<TEntity>> GetAllDbOrderedAsync(GetAllConfig<TEntity> config = null, bool asNoTracking = false)
        {
            var result = await (await GetAllDbFilteredAsync(
                config,
                asNoTracking)).ToListAsync();

            if (config == null)
                return result;

            var comparerList = await GetTEntityComparerList(config.orderByFlagsPacked);
            if (comparerList.Count() == 0)
                return result;

            result.Sort(new EntityChainComparer<TEntity>(comparerList));
            return result;
        }

        public virtual async Task<List<TModel>> GetAllFromPageAsync(GetAllConfig<TEntity> config = null, List<Func<TModel, bool>> extraModelFilters = null, Func<List<TModel>, Task> filler = null)
        {
            return await PaginatedListFactory<TModel>
                .Create(
                    await GetAllOrderedAsync(
                        config,
                        extraModelFilters,
                        filler),
                    (int)config.pageIndex, pageSize);
        }

        public virtual async Task<List<TEntity>> GetAllDbFromPageAsync(GetAllConfig<TEntity> config = null)
        {
            return await PaginatedListFactory<TEntity>
                .Create(
                    await GetAllDbOrderedAsync(config),
                    (int)config.pageIndex, pageSize);
        }

        public virtual async Task<TModel> GetOneByIdAsync(object id, Func<TModel, Task> filler = null)
        {
            var entity = await entities.FindAsync(id);
            if (entity == null)
                return null;

            var model = mapper.Map(entity, optsOne);
/*            if (filler != null)
                await filler(model);*/

            return model;
        }

        public virtual async Task<TModel> GetOneByIdAsync(object id1, object id2, Func<TModel, Task> filler = null)
        {
            var entity = await entities.FindAsync(id1, id2);
            if (entity == null)
                return null;

            var model = mapper.Map(entity, optsOne);
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
            try
            {
                await entities.AddAsync(entity);
                await db.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
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

        public virtual async Task<bool> UpdateAsync(TEntity entityToUpdate)
        {
            entities.Attach(entityToUpdate);
            db.Entry(entityToUpdate).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return true;
        }

        public virtual async Task<bool> DeleteAsync(object id)
        {
            var entityToDelete = await entities.FindAsync(id);
            if (entityToDelete == null)
                return false;

            var result = await DeleteAsync(entityToDelete);
            await db.SaveChangesAsync();

            return result;
        }

        public virtual async Task<bool> DeleteAsync(TEntity entityToDelete)
        {
            if (db.Entry(entityToDelete).State == EntityState.Detached)
                entities.Attach(entityToDelete);
            entities.Remove(entityToDelete);
            await db.SaveChangesAsync();

            return true;
        }

        protected static Task<List<Func<IQueryable<TEntity>, IQueryable<TEntity>>>> GetDynamicEntityFilterList(int whereFlagsPacked)
        {
            var filterList = new List<Func<IQueryable<TEntity>, IQueryable<TEntity>>>();

            var flagsUnpacked = FlagsUtility.GetFlagsUnpacked(whereFlagsPacked);
            if (flagsUnpacked.Count < 1)
                return Task.FromResult(filterList);

            foreach (int flag in flagsUnpacked)
            {
                if (flag == 0)
                    continue;

                var filter = new TEntity().GetDynamicEntityFilter(flag);
                if (filter == null)
                    continue;

                filterList.Add(filter);
            }

            return Task.FromResult(filterList);
        }

        protected static Task<List<Func<TEntity, TEntity, int>>> GetTEntityComparerList(int orderByFlagsPacked)
        {
            var comparerList = new List<Func<TEntity, TEntity, int>>();

            var flagsUnpacked = FlagsUtility.GetFlagsUnpacked(orderByFlagsPacked);
            if (flagsUnpacked.Count < 1)
                return Task.FromResult(comparerList);

            foreach (int flag in flagsUnpacked)
            {
                if (flag == 0)
                    continue;

                var comparer = new TEntity().GetEntityComparer(flag);
                if (comparer == null)
                    continue;

                comparerList.Add(comparer);
            }

            return Task.FromResult(comparerList);
        }

        protected static Task<List<Func<TModel, TModel, int>>> GetTModelComparerList(int orderByFlagsPacked)
        {
            var comparerList = new List<Func<TModel, TModel, int>>();

            var flagsUnpacked = FlagsUtility.GetFlagsUnpacked(orderByFlagsPacked);
            if (flagsUnpacked.Count < 1)
                return Task.FromResult(comparerList);

            foreach (int flag in flagsUnpacked)
            {
                if (flag == 0)
                    continue;

                var comparer = new TModel().GetModelComparer(flag);
                if (comparer == null)
                    continue;

                comparerList.Add(comparer);
            }

            return Task.FromResult(comparerList);
        }

    }
}
