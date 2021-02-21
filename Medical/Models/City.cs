using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.Models
{
    public class City
    {
        [Key]
        public int City_ID { get; set; }

        
        [Required(ErrorMessage = "Please Select State !!!")]
        [Display(Name = "State Name")]
        public int State_ID { get; set; }

        [Required(ErrorMessage = "Please Enter City Name !!!")]
        [Display(Name = "City Name :")]
        public string City_Name { get; set; }

        [NotMapped]
        
        public string State_Name { get; set; }
    }
}
