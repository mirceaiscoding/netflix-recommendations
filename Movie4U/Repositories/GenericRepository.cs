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
using AutoMapper;
using Movie4U.ExtensionMethods;
using Movie4U.Configurations;

namespace Movie4U.Repositories
{
    public class GenericRepository<TEntity, TModel> : IGenericRepository<TEntity, TModel>
        where TEntity : EntitiesModelsBase<TEntity, TModel>, IEntity<TEntity>, new()
        where TModel : EntitiesModelsBase<TEntity, TModel>, IModel<TModel>, new()
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
                .IncludeMultiple(config.asSplitQuery, config.includers);
        }

        public virtual async Task<List<TModel>> GetAllOrderedAsync(GetAllConfig<TEntity> config = null, List<Func<TModel, bool>> extraModelFilters = null, Func<List<TModel>, Task> filler = null)
        {
            /*var got = await GetAllDbFilteredAsync(config, true);
            foreach (var entity in got)
                Console.WriteLine(entity.ToString());*/

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

        public virtual async Task<TModel> GetOneByIdAsync(GetOneConfig<TEntity> config)
        {
            var entity = await entities
                .IncludeMultiple(
                    config.asSplitQuery,
                    config.includers)
                .FirstOrDefaultByPropertyAsync(
                    config.filterPropertySelectors,
                    config.filterValuesToMatch);
            
            if (entity == null)
                return null;

            var model = mapper.Map(entity, optsOne);
/*            if (filler != null)
                await filler(model);*/

            return model;
        }

        public virtual async Task<TEntity> GetOneDbByIdAsync(GetOneConfig<TEntity> config)
        {
            return await entities
                .IncludeMultiple(
                    config.asSplitQuery,
                    config.includers)
                .FirstOrDefaultByPropertyAsync(
                    config.filterPropertySelectors,
                    config.filterValuesToMatch);
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

        public virtual async Task<bool> DeleteAsync(params object[] ids)
        {
            var entityToDelete = await entities.FindAsync(ids);
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

                var filter = new TEntity().GetDynamicFilter(flag);
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

                var comparer = new TEntity().GetComparer(flag);
                if (comparer == null)
                    continue;

                comparerList.Add(comparer);
            }

            comparerList.Add((e1, e2) => e1.GetIds().CompareTo(e2.GetIds()));

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

                var comparer = new TModel().GetComparer(flag);
                if (comparer == null)
                    continue;

                comparerList.Add(comparer);
            }

            comparerList.Add((m1, m2) => m1.GetIds().CompareTo(m2.GetIds()));

            return Task.FromResult(comparerList);
        }

    }
}
