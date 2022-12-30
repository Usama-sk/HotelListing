using System.ComponentModel.DataAnnotations;

namespace DataModels.Models
{
    public class CreateCityDTO
    {
        [Required]
        [StringLength(maximumLength:50, ErrorMessage ="Country Name is Too Long")]
        public string Name { get; set; }
       
        public int CountryId { get; set; }


    }

    public class UpdateCityDTO : CreateCityDTO
    {
 
    }
    public class CityDTO: CreateCityDTO
    {
        public int Id { get; set; }
        public CountryDTO Country { get; set; }
    }


}
