using Microsoft.AspNetCore.Mvc;
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
        private ICustomerData _customerData;
        public CustomerController(ICustomerData customerData)
        {
            _customerData = customerData;

        }

        /// <summary>
        /// Gets all customers visible in the account of the current user
        /// </summary>
        /// <returns></returns>
        [HttpGet("")] //  ./api/customers
        public IActionResult GetAll()
        {
            var customers = _customerData.GetAll(0, 50, "");
            var models = customers.Select(c => new CustomerDisplayViewModel(c)
           );
            return Ok(models); 
           
        } 
        /// <summary>
        /// Retrieves a single customer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

