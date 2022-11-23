using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SimpleCrm.WebApi.Models
{
    public class CustomerCreateViewModel
    {
        [Required, MinLength(1), MaxLength(30)]
        public string FirstName { get; set; }
        [Required, MinLength(1), MaxLength(30)]
        public string LastName { get; set; }
        [Required, MinLength(1), MaxLength(10)]
        public string PhoneNumber { get; set; }
        [Required, MinLength(1), MaxLength(30)]
        public string EmailAddress { get; set; }
        public InteractionMethodType PreferredContactMethod { get; set; }
    }
}
