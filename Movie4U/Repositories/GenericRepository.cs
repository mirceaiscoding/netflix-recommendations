using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie4U.EntitiesModels.Entities;
using Movie4U.Repositories.IRepositories;
using Movie4U.Utilities;
using Movie4U.EntitiesModels;
using System;

namespace Movie4U.Repositories
{
    public class GenericRepository<TEntity, TModel> : IGenericRepository<TEntity, TModel>
        where TEntity : EntitiesModelsBase<TEntity, TModel>, new()
        where TModel : EntitiesModelsBase<TEntity, TModel>, new()
    {
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


        private static Task<List<Func<TEntity, bool>>> GetFilterList(int whereFlagsPacked)
        {
            var flagsUnpacked = FlagsUtility.GetFlagsUnpacked(whereFlagsPacked);
            var filterList = new List<Func<TEntity, bool>>();
            if (flagsUnpacked.Count > 0)
                foreach (int flag in flagsUnpacked)
                    filterList.Add(new TEntity().GetFilter(flag));
            return Task.FromResult(filterList);
        }

        private static Task<List<Func<TEntity, object>>> GetOrderingCriteriaList(int orderByFlagsPacked)
        {
            var flagsUnpacked = FlagsUtility.GetFlagsUnpacked(orderByFlagsPacked);
            var filterList = new List<Func<TEntity, object>>();
            if (flagsUnpacked.Count > 0)
                foreach (int flag in flagsUnpacked)
                    filterList.Add(new TEntity().GetOrderingCriteria(flag));
            return Task.FromResult(filterList);
        }


        public virtual async Task<IQueryable<TEntity>> GetAllDbQueryableAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageNumber = 1)
        {
            var filterList = await GetFilterList(whereFlagsPacked);
            var orderingCriteriaList = await GetOrderingCriteriaList(orderByFlagsPacked);

            IQueryable<TEntity> result = entities;
            foreach (var filter in filterList)
                result = result.Where(entity => filter(entity));

/*            if (orderingCriteriaList.Count() == 0)*/
                return result;

/*            IOrderedEnumerable<TEntity> resultOrdered = result.OrderBy(orderingCriteriaList[0]);
            for (int i = 1; i < orderingCriteriaList.Count(); i++)
                resultOrdered = resultOrdered.ThenBy(orderingCriteriaList[i]);

            return resultOrdered;*/
        }

        public virtual List<TModel> GetAll(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageNumber = 1)
        {
            return entities
                .AsNoTracking()
                .Select(entity => 
                    EntitiesModelsFactory<TEntity, TModel>.getModel(entity))
                .ToList();
        }

        public virtual async Task<List<TModel>> GetAllAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageNumber = 1)
        {
            return await entities
                .AsNoTracking()
                .Select(entity =>
                    EntitiesModelsFactory<TEntity, TModel>.getModel(entity))
                .ToListAsync();
        }

        public virtual IEnumerable<TEntity> GetAllDb(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageNumber = 1)
        {
            return entities;
        }

        public virtual async Task<List<TEntity>> GetAllDbAsync(int orderByFlagsPacked = 0, int whereFlagsPacked = 0, int? pageNumber = 1)
        {
            return await entities.ToListAsync();
        }

        public virtual TModel GetOneById(object id)
        {
            var entity = entities.Find(id);

            return CastUtility.ToModel<TEntity, TModel>(entity);
        }

        public virtual async Task<TModel> GetOneByIdAsync(object id)
        {
            var entity = await entities.FindAsync(id);

            return CastUtility.ToModel<TEntity, TModel>(entity);
        }

        public virtual TModel GetOneById(object id1, object id2)
        {
            var entity = entities.Find(id1, id2);

            return CastUtility.ToModel<TEntity, TModel>(entity);
        }

        public virtual async Task<TModel> GetOneByIdAsync(object id1, object id2)
        {
            var entity = await entities.FindAsync(id1, id2);

            return CastUtility.ToModel<TEntity, TModel>(entity);
        }

        public virtual TEntity GetOneDbById(object id)
        {
            return entities.Find(id);
        }

        public virtual async Task<TEntity> GetOneDbByIdAsync(object id)
        {
            return await entities.FindAsync(id);
        }

        public virtual TEntity GetOneDbById(object id1, object id2)
        {
            return entities.Find(id1, id2);
        }

        public virtual async Task<TEntity> GetOneDbByIdAsync(object id1, object id2)
        {
            return await entities.FindAsync(id1, id2);
        }

        public virtual void Insert(TEntity entity)
        {
            entities.Add(entity);
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            await entities.AddAsync(entity);
            await db.SaveChangesAsync();
            return entity;
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

    }
}
