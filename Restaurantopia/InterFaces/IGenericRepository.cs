using System.Linq.Expressions;

namespace Restaurantopia.InterFaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync ( string[] includes = null, Expression<Func<T, bool>> filter = null );
        Task<T> GetByIdAsync ( int id );
        Task<T> AddAsync ( T entity );
        Task<T> UpdateAsync ( T entity );
        Task DeleteAsync ( int id );
    }
}
