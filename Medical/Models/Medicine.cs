using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Medical.Models
{
    public class Medicine
    {
        [Key]
        public int Medicine_ID { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please Enter Medicine Name !!")]
        public string Medicine_Name { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Please Enter Medicine Price !!")]
        public string Medicine_Price { get; set; }

        [Display(Name = "Brand")]
        [Required(ErrorMessage = "Please Enter Brand !!")]
        public string Medicine_Brand { get; set; }

        [Display(Name = "Strip Tablets")]
        [Required(ErrorMessage = "Please Enter Tablets !!")]
        public string Medicine_Strip_Tablets { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "Please Select Image !!")]
        public string Medicine_Image { get; set; }

        [Display(Name = "Content")]
        [Required(ErrorMessage = "Please Enter Medicine Content !!")]
        public string Medicine_Content { get; set; }

        [Display(Name = "Description ")]
        [Required(ErrorMessage = "Please Enter Description !!")]
        public string Medicine_Description { get; set; }
        //public string SearchTerm { get; set; }



        // public HttpPostedFileBase ImageFile { get; set; }
    }
}
