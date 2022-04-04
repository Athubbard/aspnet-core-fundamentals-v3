using Microsoft.AspNetCore.Mvc;
using SimpleCrm.Web.Models;
using System;

namespace SimpleCrm.Web.Controllers
{
    public class HomeController : Controller
    {
        private ICustomerData _customerData;
        private readonly IGreeter _greeter;

        public HomeController(ICustomerData customerData, IGreeter greeter)
        {
            _customerData = customerData;
            _greeter = greeter;
        }

        public IActionResult Details(int id)
        {
            var customer = _customerData.Get(id);
            if (customer == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(customer);

        }
        [HttpGet()]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost()]
        public IActionResult Create(CustomerEditViewModel model)
        {
            var customer = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                OptInNewsletter = model.OptInNewsletter,
                Type = model.Type
            };
            _customerData.Save(customer);

            return View("Details", customer);
        }
        public IActionResult Index()
        {

            var model = new HomePageViewModel
            {
                CurrentMessage = _greeter.GetGreeting(),
                Customers = _customerData.GetAll()
            };

            return View(model);
        }
    }
}