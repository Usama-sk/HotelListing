using HotelListing.Data;
using HotelListing.DataServiecsLayer;
using HotelListing.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace HotelListing.Repository
{
    public class GenericRepository<T>: IGenericRepository<T> where T : class
    {

        private readonly AppDBContext _appDBContext;
        private DbSet<T> _dbSet;
        public GenericRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
            _dbSet = _appDBContext.Set<T>();

        }

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
        }




        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _appDBContext.Entry(entity).State = EntityState.Modified;
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }


        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = _dbSet;
            if (expression != null)
            {

                query = query.Where(expression);

            }

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);

                }

            }

            if (orderBy != null)
            {   
                    query = orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IPagedList<T>> GetPagedList(RequestParams requestParams = null,List<string> includes = null)
        {
            IQueryable<T> query = _dbSet;
            

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);

                }

            }

            return await query.AsNoTracking().ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
        }
        public async Task<T> GetbyId(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = _dbSet;
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);

                }

            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

     
    }


}

