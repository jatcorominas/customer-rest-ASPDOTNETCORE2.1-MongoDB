using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<ActionResult> GetCustomers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<Customer> customers = await _customerService.Get();

            if (customers == null)
            {
                return NotFound();
            }

            return Ok(customers);
        }

        // GET: api/Customers/5
        [HttpGet("{id:length(24)}", Name = "GetCustomerById")]
        public async Task<ActionResult> GetCustomerById([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _customerService.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // GET: api/Customers/age/30
        [HttpGet("age/{age}")]
        public async Task<ActionResult> GetCustomerByAge([FromRoute] int age)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<Customer> customers = await _customerService.GetByAge(age);

            if (customers == null)
            {
                return NotFound();
            }

            return Ok(customers);
        }

        // POST: api/Customers/create
        [HttpPost("create")]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _customerService.Create(customer);

            return CreatedAtAction("GetCustomer", new { Id = customer.Id.ToString() }, customer);
        }

        // PUT: api/Customers/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> PutCustomer([FromRoute] string id, [FromBody] Customer customerIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _customerService.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }

            await _customerService.Update(id, customerIn);

            return Ok( customerIn);

        }

        // DELETE: api/Customers/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _customerService.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            await _customerService.Remove(customer.Id);

            return Ok("Successfully deleted customer");
        }

        // DELETE: api/Customers/delete
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAll()
        {
            await _customerService.RemoveAll();

            return Ok("Deleted All Customers");
        }
    }


}