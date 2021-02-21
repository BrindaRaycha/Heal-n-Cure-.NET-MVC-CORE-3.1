using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.Models
{
    public class Doctor
    {
        [Key]
        public int Doctor_ID { get; set; }

        [Display(Name = "Name : ")]
        public string Doctor_Name { get; set; }

        [Display(Name = "Email : ")]
        public string Doctor_Email { get; set; }

        [Display(Name = "Gender : ")]
        public string Doctor_Gender { get; set; }

        [Display(Name = "Password : ")]
        public string Doctor_Password { get; set; }

        public string Doctor_Fees { get; set; }

        [Display(Name = "Contact : ")]
        public string Doctor_Contact { get; set; }

        [Display(Name = "State : ")]
        [NotMapped]
        public int State_ID { get; set; }
        [NotMapped]

        [Display(Name = "City : ")]
        public int City_ID { get; set; }
        public bool Doctor_IsActive { get; set; }
        //public bool Doctor_IsApproved { get; set; }

        [Display(Name = "Category : ")]
        public int Category_ID { get; set; }

        [Display(Name = "Degreee : ")]
        public string Doctor_Degree { get; set; }

      
        [Display(Name = "Profile : ")]
        public string Doctor_Profile { get; set; }
        public string Doctor_Education { get; set; }
        public string Doctor_Experience { get; set; }


        //public string Doctor_Fees { get; set; }

        [NotMapped]
        public string State_Name { get; set; }
        [NotMapped]
        public string City_Name { get; set; }
        [NotMapped]
        public string Category_Name { get; set; }
        [NotMapped]
        public string Clinic_Name { get; set; }
        [NotMapped]
        public string Clinic_Time { get; set; }

        [NotMapped]
        public string Clinic_Contact { get; set; }
        [NotMapped]
        public string Clinic_Address { get; set; }

        [NotMapped]
        public string Clinic_Pincode { get; set; }
        [NotMapped]
        public int Clinic_ID { get; set; }

    }
}
