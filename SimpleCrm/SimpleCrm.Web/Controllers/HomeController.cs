using Microsoft.AspNetCore.Mvc;
using SimpleCrm.Web.Models;

namespace SimpleCrm.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string id)
        {

            var model = new CustomerModel {
                Id=1, 
                FirstName= "John", 
                LastName="Doe",
                PhoneNumber="314-123-4567" 
            };
            return View(model);
        }
    }
}