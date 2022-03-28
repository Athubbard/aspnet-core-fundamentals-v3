using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.Web.Controllers
{
    [Route("about")]
    public class AboutController
    {
       [Route("phone")]
        public string Phone()
        {
            return "413-123-1234";
           
            
        }
       [Route("address")]
        public string Address()
        {
            return "USA";
        }
    }
}
