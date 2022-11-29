using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using SimpleCrm.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;


namespace SimpleCrm.WebApi.ApiControllers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        private readonly ICustomerData _customerData;
        private readonly LinkGenerator _linkGenerator;

        public CustomerController(ICustomerData customerData, LinkGenerator linkGenerator)
        {
            _customerData = customerData;
            _linkGenerator = linkGenerator;
        }

        /// <summary>
        /// Gets all customers visible in the account of the current user
        /// </summary>
        /// <returns></returns>
        [HttpGet("", Name = "GetCustomers")] //  ./api/customers
        public IActionResult GetCustomers([FromQuery] CustomerListParameters resourceParameters)
        {
            var customers = _customerData.GetAll(resourceParameters);
            var models = customers.Select(c => new CustomerDisplayViewModel(c));
            

            if (resourceParameters.Page < 1 || resourceParameters.Take <= 0)
            {
                return UnprocessableEntity("Invalid Page");
            };
            var pagination = new PaginationModel
            {
                Next = GetCustomerResourceUri(resourceParameters, 1),
                Previous = GetCustomerResourceUri(resourceParameters, -1)
            };

            
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagination));
           

            return Ok(models); 
           
        }
        private string GetCustomerResourceUri(CustomerListParameters listParameters, int pageAdjust)
        {
            if (listParameters.Page + pageAdjust <= 0)
                return null;
            return _linkGenerator.GetPathByName(this.HttpContext, "GetCustomers", values: new
            {
                page = listParameters.Page + pageAdjust,
                take = listParameters.Take,
                orderBy = listParameters.Orderby,
                
            }); ;
        }
        
        /// Retrieves a single customer by id
        
        [HttpGet("{id}")] //  ./api/customers/:id
       
        public IActionResult Get(int id)
        {
            var customer = _customerData.Get(id);
            if (customer == null)
            {
                return NotFound();
               
            }
            var model = new CustomerDisplayViewModel(customer);
            
            return Ok(model);
        }
        [HttpPost("")]
        public IActionResult Create([FromBody] CustomerCreateViewModel model)
        {
             if (model == null)
            {
                return BadRequest();
            }

             if (!ModelState.IsValid)
            {
                return StatusCode(422, new ValidationStateModel(ModelState));
            }
            var customer = new Customer();
            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.EmailAddress = model.EmailAddress;
            customer.PhoneNumber = model.PhoneNumber;
            customer.PreferredContactMethod = model.PreferredContactMethod;

            _customerData.Add(customer);
            _customerData.Commit();

            
            return this.Ok(new CustomerDisplayViewModel(customer));
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CustomerUpdateViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return StatusCode(422, new ValidationStateModel(ModelState));
            }
            var editCustomer = _customerData.Get(id);
            editCustomer.FirstName = model.FirstName;
            editCustomer.LastName = model.LastName;
            editCustomer.PhoneNumber = model.PhoneNumber;
            editCustomer.OptInNewsletter = model.OptInNewsletter;
            editCustomer.Type = model.Type;

            _customerData.Commit();
            return this.Ok(new CustomerDisplayViewModel (editCustomer));

        }
        [HttpDelete("{id}")] 
        public IActionResult Delete(int id)
        {
            
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return StatusCode(422, new ValidationStateModel(ModelState)); ;
            }
            var customer = _customerData.Get(id);
            if (customer == null)
            {
                return NotFound(); 
            }
            _customerData.Delete(id);
            _customerData.Commit();
            return this.Ok(); 
        }
    }
}

