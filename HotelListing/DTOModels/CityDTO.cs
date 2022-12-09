using HotelListing.Data;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.DTOModels
{
    public class CreateCityDTO
    {
        [Required]
        [StringLength(maximumLength:50, ErrorMessage ="Country Name is Too Long")]
        public string Name { get; set; }
        [Required]
        public int CountryId { get; set; }


    }

    public class CityDTO: CreateCityDTO
    {
        public int Id { get; set; }
        public CountryDTO Country { get; set; }
    }


}
