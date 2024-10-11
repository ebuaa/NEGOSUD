using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEGOSUDAPI.Models.Entities;
using NEGOSUDAPI.Services.CustomersServices;

namespace NEGOSUDAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly ICustomersService _customersService;

        public CustomersController(ICustomersService customersService)
        {
            _customersService = customersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customersService.GetAllCustomersAsync();
            return Ok(customers);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var customer = await _customersService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            await _customersService.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(Details), new { id = customer.CustomerID }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return BadRequest();
            }
            await _customersService.UpdateCustomerAsync(customer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _customersService.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}
