using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movie4U.Entities;

namespace Movie4U.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal Movie4UContext _context;
        internal DbSet<TEntity> entities;

        public GenericRepository(Movie4UContext context)
        {
            this._context = context;
            entities = _context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetQueryable()
        {
            return entities;
        }

        public virtual IEnumerable<TEntity> GetAllDb()
        {
            return entities;
        }

        public virtual TEntity GetByID(object id)
        {
            return entities.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            entities.Add(entity);
        }

        public virtual async Task DeleteAsync(object id)
        {
            TEntity entityToDelete = await entities.FindAsync(id);
            await DeleteAsync(entityToDelete);
        }

        public virtual async Task DeleteAsync(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                entities.Attach(entityToDelete);
            }
            entities.Remove(entityToDelete);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entityToUpdate)
        {
            entities.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAllDbAsync()
        {
            return await entities.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await entities.FindAsync(id);
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await entities.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

    }
}
