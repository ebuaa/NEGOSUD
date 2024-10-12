using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NEGOSUDAPI.Services.OrdersServices;
using NEGOSUDAPI.Models.Entities;
namespace NEGOSUDAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            await _orderService.AddOrderAsync(order);
            return CreatedAtAction(nameof(Details), new { id = order.OrderID }, order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] Order order)
        {
            if (id != order.OrderID)
            {
                return BadRequest();
            }
            await _orderService.UpdateOrderAsync(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}
