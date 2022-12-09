using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace HotelListing.IRepository
{
    public interface IGenericRepository<T> where T : class
    {

        Task<IList<T>> GetAll(
             Expression<Func<T, bool>> expression = null,
             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
             List<string> includes = null);
        Task<T> GetbyId(Expression<Func<T, bool>> expression, List<string> includes = null);
        Task Add(T entity);
        Task Delete(int id);

        void DeleteRange(IEnumerable<T> entities);
        void Update(T entity);
    }
}
