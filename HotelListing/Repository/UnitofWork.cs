using HotelListing.Data;
using HotelListing.DataServiecsLayer;
using HotelListing.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HotelListing.Repository
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
