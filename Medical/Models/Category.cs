using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.Models
{
    public class Category
    {
        [Key]
        public int Category_ID { get; set; }

        [Required(ErrorMessage = "Please Enter Category Name !!!")]
        [Display(Name = "Category Name :")]
        public string Category_Name { get; set; }

        [Required(ErrorMessage = "Please Enter Category Description !!!")]
        [Display(Name = "Category Description :")]
        public string Category_Description { get; set; }

        [Required(ErrorMessage = "Please Enter Category Profile !!!")]
        [Display(Name = "Category Pofile :")]
        public string Category_Profile { get; set; }

        [Required(ErrorMessage = "Please Enter Category SubDescription !!!")]
        [Display(Name = "Category SubDescription :")]
        public string Category_SubDescription { get; set; }

        [Required(ErrorMessage = "Please Enter Main Profile !!!")]
        [Display(Name = "Category Main Pofile :")]
        public string Category_MainProfile { get; set; }
    }
}
