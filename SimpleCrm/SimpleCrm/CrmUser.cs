using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace SimpleCrm
{
    public class CrmUser : IdentityUser
    {
        [MaxLength(256)]
        public string DisplayName { get; set; }
        
        

    }
    
}
