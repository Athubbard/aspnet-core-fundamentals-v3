using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.Web.Controller
{
    public class HomeController
    {
        public string Index(string id)
        {
            return "Hello from a controller " + id;
        }
    }
}
