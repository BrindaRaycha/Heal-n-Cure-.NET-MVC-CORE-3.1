using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.Models
{
    public class Login
    {
        [Key]
        public int Admin_ID { get; set; }

        [Display(Name = "Name : ")]
        public string Admin_Name { get; set; }

        [Display(Name = "Password : ")]

        [Required(ErrorMessage = "Please Enter Password !!")]
        [DataType(DataType.Password)]
        public string Admin_Password { get; set; }

        [Display(Name = "Email : ")]

        [Required(ErrorMessage = "Please Enter Valid Email ID !!")]
        [DataType(DataType.EmailAddress)]
        public string Admin_Email { get; set; }    
    }
}
