using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DataModels.Models;

namespace DataServiceLayer.Configurations.Entities
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {

            builder.HasData(
              new City
              {
                  Id = 1,
                  Name = "Karachi",
                  CountryId = 1
              }, new City
              {
                  Id = 2,
                  Name = "Lahore",
                  CountryId = 1
              }, new City
              {
                  Id = 3,
                  Name = "Islamabad",
                  CountryId = 1
              },
              new City
              {
                  Id = 4,
                  Name = "Dehli",
                  CountryId = 2

              },
              new City
              {
                  Id = 5,
                  Name = "Mumbai",
                  CountryId = 2

              },
              new City
              {
                  Id = 6,
                  Name = "Bombay",
                  CountryId = 2

              },
              new City
              {
                  Id = 7,
                  Name = "Dhaka",
                  CountryId = 3
              },
              new City
              {
                  Id = 8,
                  Name = "Kathmandu",
                  CountryId = 4
              },
              new City
              {
                  Id = 9,
                  Name = "Beijing",
                  CountryId = 5
              },
              new City
              {
                  Id = 10,
                  Name = "Tokyo",
                  CountryId = 6
              },
              new City
              {
                  Id = 11,
                  Name = "Ankara",
                  CountryId = 7
              },
              new City
              {
                  Id = 12,
                  Name = "Riyadh",
                  CountryId = 8
              });
        }
    }
}
