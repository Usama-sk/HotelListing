using AutoMapper;
using HotelListing.Data;
using HotelListing.DTOModels;

namespace HotelListing.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Country, CreateCountryDTO>().ReverseMap();
            CreateMap<City, CityDTO>().ReverseMap();
            CreateMap<City, CreateCityDTO>().ReverseMap();
            CreateMap<AppUser, UserDTO>().ReverseMap();
        }
    }
}
