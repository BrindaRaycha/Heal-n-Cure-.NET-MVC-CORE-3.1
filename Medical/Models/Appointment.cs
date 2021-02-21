using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.Models
{
    public class Appointment
    {
        [Key]
        public int Appointment_ID { get; set; }

        [Required(ErrorMessage = "Please Select Date !!!")]
       
        public string Appointment_Date { get; set; }
        [Required(ErrorMessage = "Please Select Doctor !!!")]
        public int Doctor_ID { get; set; }

        [NotMapped]
        public string Doctor_Name { get; set; }
        public int Patient_ID { get; set; }
        [Required(ErrorMessage = "Please Select Category Of Doctor !!!")]
        public int Category_ID { get; set; }

        [Required(ErrorMessage = "Please Select Time !!!")]
        public string Appointment_Time { get; set; }
        public string Appointment_Status { get; set; }

        [Required(ErrorMessage = "Please Enter Your Problem Message !!!")]
        public string Appointment_Msg { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Please Enter Your Name !!!")]
        public string Patient_Name { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Please Enter Your Contact !!!")]
        public string Patient_Contact { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Please Enter Valid Email !!!")]
        public string Patient_Email { get; set; }
    }
}
