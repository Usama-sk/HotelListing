using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HotelListing.Data;

namespace HotelListing.Configurations.Entities
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
               new Country
               {
                   Id = 1,
                   Name = "Pakistan"
               },
               new Country
               {
                   Id = 2,
                   Name = "India"
               },
               new Country
               {
                   Id = 3,
                   Name = "Bangladesh"
               },
               new Country
               {
                   Id = 4,
                   Name = "Nepal"
               },
               new Country
               {
                   Id = 5,
                   Name = "China"
               },
               new Country
               {
                   Id = 6,
                   Name = "Japan"
               },
               new Country
               {
                   Id = 7,
                   Name = "Turkey"
               },
               new Country
               {
                   Id = 8,
                   Name = "Saudia Arab"
               }
           );
        }
    }
}
