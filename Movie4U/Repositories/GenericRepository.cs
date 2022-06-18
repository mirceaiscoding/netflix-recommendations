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


        public virtual IQueryable<TEntity> GetAllDbFiltered(GetAllConfig<TEntity> config = null, bool asNoTracking = false)
        {
            IQueryable<TEntity> result = entities;
            if (asNoTracking)
                result = result.AsNoTracking();

            if (config == null)
                return result;

            var filterList = GetDynamicEntityFilterList(config.whereFlagsPacked);
            foreach (var filter in filterList)
                result = filter(result);

            if (config.extraEntityFilters != null)
                foreach (var filter in config.extraEntityFilters)
                    result = filter(result);

            if (config.includers == null || config.includers.Count() == 0)
                return result;

            return result
                .IncludeMultiple(config.asSplitQuery, config.includers);
        }

        public virtual List<TModel> GetAllOrdered(GetAllConfig<TEntity> config = null, List<Func<TModel, bool>> extraModelFilters = null)
        {
            var result = mapper.Map(
                GetAllDbFiltered(config, true),
                optsAll);

            if (extraModelFilters != null)
                foreach (var filter in extraModelFilters)
                    result = result
                        .Where(model => filter(model))
                        .ToList();

            if (config == null)
                return result;

            var comparerList = GetTModelComparerList(config.orderByFlagsPacked);
            if (comparerList.Count() == 0)
                return result;

            result.Sort(new ModelChainComparer<TModel>(comparerList));
            return result;
        }

        public virtual async Task<List<TEntity>> GetAllDbOrderedAsync(GetAllConfig<TEntity> config = null, bool asNoTracking = false)
        {
            var result = await GetAllDbFiltered(
                config,
                asNoTracking).ToListAsync();

            if (config == null)
                return result;

            var comparerList = GetTEntityComparerList(config.orderByFlagsPacked);
            if (comparerList.Count() == 0)
                return result;

            result.Sort(new EntityChainComparer<TEntity>(comparerList));
            return result;
        }

        public virtual async Task<List<TModel>> GetAllFromPageAsync(GetAllConfig<TEntity> config = null, List<Func<TModel, bool>> extraModelFilters = null)
        {
            return await PaginatedListFactory<TModel>
                .Create(
                    GetAllOrdered(
                        config,
                        extraModelFilters),
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

        protected static List<Func<IQueryable<TEntity>, IQueryable<TEntity>>> GetDynamicEntityFilterList(int whereFlagsPacked)
        {
            var flagsUnpacked = FlagsUtility.GetFlagsUnpacked(whereFlagsPacked);
            if (!flagsUnpacked.Any())
                return new();

            var filterList = new List<Func<IQueryable<TEntity>, IQueryable<TEntity>>>();
            foreach (int flag in flagsUnpacked)
            {
                if (flag == 0)
                    continue;

                var filter = new TEntity().GetDynamicFilter(flag);
                if (filter == null)
                    continue;

                filterList.Add(filter);
            }

            return filterList;
        }

        protected static List<Func<TEntity, TEntity, int>> GetTEntityComparerList(int orderByFlagsPacked)
        {
            var flagsUnpacked = FlagsUtility.GetFlagsUnpacked(orderByFlagsPacked);
            if (!flagsUnpacked.Any())
                return new();

            var comparerList = new List<Func<TEntity, TEntity, int>>();
            foreach (int flag in flagsUnpacked)
            {
                if (flag == 0)
                    continue;

                var comparer = new TEntity().GetComparer(flag);
                if (comparer == null)
                    continue;

                comparerList.Add(comparer);
            }

            comparerList.Add((e1, e2) =>
                EntityIdsUtility<TEntity>.Get(e1)
                .CompareTo(
                    EntityIdsUtility<TEntity>.Get(e2)));

            return comparerList;
        }

        protected static List<Func<TModel, TModel, int>> GetTModelComparerList(int orderByFlagsPacked)
        {
            var flagsUnpacked = FlagsUtility.GetFlagsUnpacked(orderByFlagsPacked);
            if (!flagsUnpacked.Any())
                return new();

            var comparerList = new List<Func<TModel, TModel, int>>();
            foreach (int flag in flagsUnpacked)
            {
                if (flag == 0)
                    continue;

                var comparer = new TModel().GetComparer(flag);
                if (comparer == null)
                    continue;

                comparerList.Add(comparer);
            }

            comparerList.Add((m1, m2) =>
                ModelIdsUtility<TModel>.Get(m1)
                .CompareTo(
                    ModelIdsUtility<TModel>.Get(m2)));

            return comparerList;
        }

    }
}
