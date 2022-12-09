using System.Security.Claims;
using System.Threading.Tasks;
using System;
using HotelListing.Data;

namespace HotelListing.IRepository
{
    public interface IUnitofWork : IDisposable
    {
        IGenericRepository<City> Cities { get; }
        IGenericRepository<Country> Countries { get; }

        Task Save();
    }
}
