using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels.Models
{
    public class Country
    {
     
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual  IList<City> Cities { get; set; }
    }
}
