using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.Web.Controller
{
    
    public class AboutController
    {
       
        public string Phone()
        {
            return "413-123-1234";
           
            
        }
       
        public string Address()
        {
            return "USA";
        }
    }
}
