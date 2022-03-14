using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCrm
{
    public class ConfigurationGreeter : IGreeter
    {
        private readonly IConfiguration configuration;

        public ConfigurationGreeter(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string GetGreeting()
        {
            var greeting = configuration["Greeting"];

            return greeting;
        
        }
    }
}
