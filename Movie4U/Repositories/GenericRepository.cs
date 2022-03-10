using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movie4U.Entities;

namespace Movie4U.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
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

        public virtual IQueryable<TEntity> GetQueryable()
        {
            return entities;
        }

        public virtual IEnumerable<TEntity> GetAllDb()
        {
            return entities;
        }

        public async Task<List<TEntity>> GetAllDbAsync()
        {
            return await entities.AsNoTracking().ToListAsync();
        }

        public virtual TEntity GetByID(object id)
        {
            return entities.Find(id);
        }

        public async Task<TEntity> GetOneDbByIdAsync(object id)
        {
            return await entities.FindAsync(id);
        }

        public virtual void Insert(TEntity entity)
        {
            entities.Add(entity);
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
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
            TEntity entityToDelete = await entities.FindAsync(id);
            await DeleteAsync(entityToDelete);
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
