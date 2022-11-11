using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.WebApi.Models
{
    public class CustomerUpdateViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public InteractionMethodType PreferredContactMethod { get; set; }
        public CustomerType Type { get; set; }
        public bool OptInNewsletter { get; set; }
    }
}
