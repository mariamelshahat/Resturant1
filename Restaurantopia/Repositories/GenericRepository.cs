using Microsoft.EntityFrameworkCore;
using Restaurantopia.InterFaces;
using Restaurantopia.Models;
using System.Linq.Expressions;

namespace Restaurantopia.Repositories
{
    //im
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private MyDbContext _db;
        private DbSet<T> _dbSet;
        public GenericRepository ( MyDbContext db )
        {
            _db = db;
            _dbSet = _db.Set<T> ();
        }

        public async Task<T> AddAsync ( T entity )
        {
            await _dbSet.AddAsync ( entity );
            await _db.SaveChangesAsync ();
            return entity;
        }

        public async Task DeleteAsync ( int id )
        {
            var item = await GetByIdAsync ( id );
            _dbSet.Remove ( item );
            await _db.SaveChangesAsync ();
        }

        public async Task<IEnumerable<T>> GetAllAsync ( string[] includes = null, Expression<Func<T, bool>> filter = null )
        {
            IQueryable<T> query = _db.Set<T> ();

            // Apply includes if any
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include ( include );
                }
            }

            // Apply the filter if provided
            if (filter != null)
            {
                query = query.Where ( filter );
            }

            return await query.ToListAsync ();
        }




        public async Task<T> GetByIdAsync ( int id )
        {
            return await _dbSet.FindAsync ( id );
        }

        public async Task<T> UpdateAsync ( T entity )
        {
            _dbSet.Update ( entity );
            await _db.SaveChangesAsync ();
            return entity;
        }
    }
}
