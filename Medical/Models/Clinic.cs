using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.Models
{
    public class Clinic
    {
        [Key]
        public int Clinic_ID { get; set; }

        [Display(Name = "Clinic Name")]
        [Required(ErrorMessage = "Please Enter Clinic Name !!!")]
        public string Clinic_Name { get; set; }

        [Required(ErrorMessage = "Please Enter Clinic Address !!!")]
        [Display(Name = "Clinic Address")]
        public string Clinic_Address { get; set; }

        [Display(Name = "Clinic Pincode")]
        [Required(ErrorMessage = "Please Enter Clinic Pincode !!!")]
        public string Clinic_Pincode { get; set; }

        [Display(Name = "Clinic Contact")]
        [Required(ErrorMessage = "Please Enter Clinic Contact !!!")]
        public string Clinic_Contact { get; set; }

        [Required(ErrorMessage = "Please Enter Clinic Profile !!!")]
        public string Clinic_Profile { get; set; }
        [Display(Name = "Status")]
        public bool Clinic_IsActive { get; set; }

        [Required(ErrorMessage = "Please Select Clinic Time !!!")]
        public string Clinic_Time { get; set; }
        public int Doctor_ID { get; set; }
        public Int32 State_ID { get; set; }
        public Int32 City_ID { get; set; }
        [NotMapped]
        public string State_Name { get; set; }
        [NotMapped]
        public string City_Name { get; set; }
    }
}
