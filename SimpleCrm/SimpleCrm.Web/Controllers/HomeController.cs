using Microsoft.AspNetCore.Mvc;
using SimpleCrm.Web.Models;

namespace SimpleCrm.Web.Controllers
{
    public class HomeController : Controller
    {
        private ICustomerData _customerData;
        public HomeController(ICustomerData customerData)
        {
            _customerData = customerData;
        }
        public IActionResult Index(string id)
        {

            var model = _customerData.GetAll();
                
            return View(model);
        }
    }
}