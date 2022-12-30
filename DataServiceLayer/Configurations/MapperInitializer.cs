using AutoMapper;
using DataModels.Models;
using DataModels.DTOModels;

namespace DataServiceLayer.Configurations
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
