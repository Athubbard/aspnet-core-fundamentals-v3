using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SimpleCrm
{
    public class CustomerListParameters
    {
        public int Page { get; set; }
        public int Take { get; set; }
        public string Orderby { get; set; }
        public string Term { get; set; }
        public string LastName { get; set; }
    }

    
}
