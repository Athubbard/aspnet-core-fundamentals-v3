using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.Web.Models.RegisterModel
{
    public class RegisterUserViewModel
    {
        [Required, MaxLength(100), DisplayName("Email")]
        public string UserName { get; set; }

        [Required, MaxLength(256), DisplayName("Name")]
        public string DisplayName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
