using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.WebApi.Models
{
    public class CustomerDisplayViewModel
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public InteractionMethodType PreferredContactMethod { get; set; }
        public CustomerStatusType Status { get; set; }
        public DateTimeOffset LastContactDate { get; set; }

        public CustomerDisplayViewModel() { }
        public CustomerDisplayViewModel(Customer source)
        {
            CustomerId = source.Id;
            FirstName = source.FirstName;
            LastName = source.LastName;
            EmailAddress = source.EmailAddress;
            PhoneNumber = source.PhoneNumber;
            Status = source.Status;
            PreferredContactMethod = source.PreferredContactMethod;
            LastContactDate = source.LastContactDate;
      }

    }
}
