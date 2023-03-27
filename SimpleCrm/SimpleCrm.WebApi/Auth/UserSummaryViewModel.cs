using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SimpleCrm.WebApi.Auth
{
    public class UserSummaryViewModel
    {
        public string User { get; set; }
        public string Id { get; set; }
        public string name { get; set; }
        public string Emailaddress { get; set; }
        public string roles { get; set; }
        public string JWTtoken { get; set; }

    }
}
