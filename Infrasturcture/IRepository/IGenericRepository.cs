using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;
using X.PagedList;
using DataModels.Models;


namespace Infrasturcture.IRepository
{
    public interface IGenericRepository<T> where T : class
    {

        Task<IList<T>> GetAll(
             Expression<Func<T, bool>> expression = null,
             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
             List<string> includes = null);
        Task<IPagedList<T>> GetPagedList(
            RequestParams requestParams,
            List<string> includes = null
            );
        Task<T> GetbyId(Expression<Func<T, bool>> expression, List<string> includes = null);
        Task Add(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task Delete(int id);

        void DeleteRange(IEnumerable<T> entities);
        void Update(T entity);
    }
}
