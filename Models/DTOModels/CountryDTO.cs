using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModels.Models
{
    public class CreateCountryDTO
    {

        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Country Name is Too Long")]

        public string Name { get; set; }


    }
    public class UpdateCountryDTO : CreateCountryDTO
    {
        public IList<CreateCityDTO> Cities { get; set; }
    }
    public class CountryDTO : CreateCountryDTO
    {
        public int Id { get; set; }
        public  IList<CityDTO> Cities { get; set; }
      
    }
}
