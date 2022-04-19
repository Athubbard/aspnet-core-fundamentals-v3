using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.Web.ViewComponents
{
    public class LoginLogoutViewComponent : ViewComponent
    {
        private readonly UserManager<CrmUser> userManager;
        public LoginLogoutViewComponent(UserManager<CrmUser> userManager)
        {
            
            this.userManager = userManager;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated == false) 
            {
                return View(new CrmUser());
                 
                
            }

            var currentUser = await this.userManager.FindByNameAsync(this.User.Identity.Name);
            return View(currentUser);
        }
    }
}
