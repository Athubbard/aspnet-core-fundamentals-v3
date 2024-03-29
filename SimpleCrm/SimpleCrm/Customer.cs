﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCrm
{
    public class Customer
    {
        public int Id { get; set; }
        [Required, MinLength(1), MaxLength(30)]
        public string FirstName { get; set; }
        [Required, MinLength(1), MaxLength(30)]
        public string LastName { get; set; }
        [Required, MinLength(1), MaxLength(10)]
        public string PhoneNumber { get; set; }
        public CustomerType Type { get; set; }

        public bool OptInNewsletter { get; set; }

        [Required, MinLength(1), MaxLength(30)]
        public string EmailAddress { get; set; }
        [Required]
        public InteractionMethodType PreferredContactMethod { get; set; }
        public CustomerStatusType Status { get; set; }
        
        public DateTimeOffset LastContactDate { get; set; }
        

    }
}