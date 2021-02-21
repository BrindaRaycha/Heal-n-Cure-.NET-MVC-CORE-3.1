using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.Models
{
    public class Review
    {
        [Key]
        public int Review_ID { get; set; }

        [Display(Name = "Patient Name")]
        public int Patient_ID { get; set; }

        [Display(Name = "Doctor Name ")]
        public int Doctor_ID { get; set; }


        [Display(Name = "Review : ")]
        [Required(ErrorMessage = "Please Enter Review Message !!!")]
        public string Review_Msg { get; set; }

        [NotMapped]
        public string Doctor_Name { get; set; }

        [NotMapped]
        public string Patient_Name { get; set; }
        [NotMapped]
        public string Patient_Profile { get; set; }

        [NotMapped]
        public string City_Name { get; set; }
    }
}
