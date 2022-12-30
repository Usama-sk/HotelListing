using Infrasturcture.IRepository;
using System;
using System.Threading.Tasks;
using DataServiceLayer.DataService;
using DataModels.Models;

namespace Infrasturcture.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly AppDBContext _dbContext;

        private IGenericRepository<City> _continent;
        private IGenericRepository<Country> _country;
 
        public UnitofWork(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<Country> Countries => _country ??= new GenericRepository<Country>(_dbContext);

        public IGenericRepository<City> Cities => _continent ??= new GenericRepository<City>(_dbContext);

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
