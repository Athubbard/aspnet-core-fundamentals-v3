//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace SimpleCrm.WebApi.Controllers
//{
//    public class AuthController : Controller
//    {
//        private readonly ILogger<AuthController> _logger;

//        public AuthController(
//            ILogger<AuthController> logger
//          )
//        {
//            _logger = logger;
//        }
//        [HttpPost]
//        [Route("register")]
//        [AllowAnonymous]
//        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
//        {
//            // .. some code omitted

//            _logger.LogInformation("User created a new account with password.");
//        }
//    }
//}
