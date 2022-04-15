using Microsoft.AspNetCore.Mvc;
using SimpleCrm.Web.Models.RegisterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.Web.Controllers
{
    public class AccountController: Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Register(RegisterUserViewModel model)
        {
            var RegisterUser = new RegisterUserViewModel
            {
                UserName = model.UserName,
                DisplayName = model.DisplayName,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword

            };
            return View(model);
        }
    }
}
