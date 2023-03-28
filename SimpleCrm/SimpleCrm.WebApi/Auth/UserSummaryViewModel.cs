using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SimpleCrm.WebApi.Auth
{
    public class UserSummaryViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public IList <string> Roles { get; set; }
        public string JWTtoken { get; set; }

    }
}
