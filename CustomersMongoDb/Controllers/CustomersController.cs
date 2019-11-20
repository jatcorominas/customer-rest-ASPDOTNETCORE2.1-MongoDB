using System.Collections.Generic;
using CustomersMongoDb.Models;
using CustomersMongoDb.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomersMongoDb.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomersController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/Customers
        [HttpGet]
        public ActionResult<List<Customer>> Get() =>
            _customerService.Get();

        // GET: api/Customers/5
        [HttpGet("{id:length(24)}", Name = "GetCustomerById")]
        public ActionResult<Customer> GetById([FromRoute] string id)
        {
            var customer = _customerService.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // GET: api/Customers/age/30
        [HttpGet("age/{age}")]
        public ActionResult<List<Customer>> GetByAge([FromRoute] int age)
        {
            var customers = _customerService.GetByAge(age);

            if (customers == null)
            {
                return NotFound();
            }

            return customers;
        }

        // POST: api/Customers/create
        [HttpPost("create")]
        public ActionResult<Customer> Create([FromBody] Customer customer)
        {
            _customerService.Create(customer);

            return CreatedAtRoute("GetCustomerById", new { id = customer.Id.ToString() }, customer);
        }

        // PUT: api/Customers/5
        [HttpPut("{id:length(24)}")]
        public ActionResult<Customer> Update([FromRoute] string id, [FromBody] Customer customerIn)
        {
            var customer = _customerService.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            _customerService.Update(id, customerIn);

            return customerIn;
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete([FromRoute] string id)
        {
            var customer = _customerService.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            _customerService.Remove(customer.Id);

            return NoContent();
        }

        // DELETE: api/Customers/delete
        [HttpDelete("delete")]
        public IActionResult DeleteAll()
        {
            _customerService.RemoveAll();

            return NoContent();
        }
    }


}