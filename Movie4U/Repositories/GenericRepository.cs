using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie4U.EntitiesModels.Entities;
using Movie4U.Repositories.IRepositories;
using Movie4U.Utilities;
using Movie4U.EntitiesModels;

namespace Movie4U.Repositories
{
    public class GenericRepository<TEntity, TModel> : IGenericRepository<TEntity, TModel>
        where TEntity : EntitiesModelsBase<TEntity, TModel>
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

        public virtual IQueryable<TEntity> GetAllDbQueryable()
        {
            return entities;
        }

        public virtual List<TModel> GetAll()
        {
            var entitiesList = entities.AsNoTracking().ToList();

            return CastUtility.ToModelsList<TEntity, TModel>(entitiesList);
        }

        public virtual async Task<List<TModel>> GetAllAsync()
        {
            var entitiesList = await entities.AsNoTracking().ToListAsync();

            return CastUtility.ToModelsList<TEntity, TModel>(entitiesList);
        }

        public virtual IEnumerable<TEntity> GetAllDb()
        {
            return entities;
        }

        public virtual async Task<List<TEntity>> GetAllDbAsync()
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

        public async Task<TEntity[]> InsertMultipleAsync(TEntity[] entities)
        {
            await this.entities.AddRangeAsync(entities);
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

    }
}
