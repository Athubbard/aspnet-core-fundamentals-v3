using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SimpleCrm.Web.Models
{
    public class CustomerEditViewModel
    {
        public int Id { get; set; }
        [Display(Name = "First Name")]

        [Required, MinLength(1), MaxLength(30)]
        public string FirstName { get; set; }

        [Required, MinLength(1), MaxLength(30)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required, MinLength(1), MaxLength(10)]
        [DataType (DataType.PhoneNumber)]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }
        public bool OptInNewsletter { get; set; }
        public CustomerType Type { get; set; }

    }
}
