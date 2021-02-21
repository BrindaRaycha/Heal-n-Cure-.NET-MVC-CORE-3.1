using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.Models
{
    public class Career
    {
        [Key]
        public int Career_ID { get; set; }
        public int Patient_ID { get; set; }

        [Required(ErrorMessage = "Please Enter Your Name !!")]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Enter Only Alphabet")]
        public string Candidate_Name { get; set; }

        [Required(ErrorMessage = "Please Enter Contact!!")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Contact number")]
        public string Candidate_Contact { get; set; }

        [Required(ErrorMessage = "Please Select Your Resume !!")]
        public string Candidate_Resume { get; set; }

        [Required(ErrorMessage = "Please Enter Your Email !!")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Please enter a valid email address")]

        public string Candidate_Email { get; set; }

        [NotMapped]
        public string Patient_Name { get; set; }
    }
}
