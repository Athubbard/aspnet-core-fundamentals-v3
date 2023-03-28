using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleCrm.WebApi.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.WebApi.ApiControllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly UserManager<CrmUser> _userManager;
        private readonly JwtFactory _jwtFactory;
        public AuthController(UserManager<CrmUser> userManager)
        {
            this._userManager = userManager;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] CredentialsViewModel credentials)
        { // TODO: create a CredentialsViewModel class in the next assignment
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(credentials.EmailAddress);
            if (user == null)
            {
                return UnprocessableEntity("Invalid username or password.");
            }
            var validPassword = await _userManager.CheckPasswordAsync(user, credentials.Password);
            var userModel = await GetUserData(user);
            return Ok(userModel);

        }

       
       public async Task<UserSummaryViewModel> GetUserData(CrmUser crmUser)
        {
            if (crmUser == null) 
                return null;
            var roles = await _userManager.GetRolesAsync(crmUser);
            if (roles.Count == 0)
            {
                roles.Add("prospect");
            }
            // TODO: add GetUserData method (see lesson below)
            var jwt = await _jwtFactory.GenerateEncodedToken(crmUser.UserName,
       _jwtFactory.GenerateClaimsIdentity(crmUser.UserName, crmUser.Id.ToString()));
            var viewModel = new UserSummaryViewModel();
            viewModel.UserId = crmUser.Id;
            viewModel.Name = crmUser.UserName;
            viewModel.EmailAddress = crmUser.Email;
            viewModel.Roles = roles.ToArray();
            viewModel.JWTtoken = jwt;
            //returns a UserSummaryViewModel containing a JWT and other user info
            return viewModel; 
       }

        
    }
}

