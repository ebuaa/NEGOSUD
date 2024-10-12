using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NEGOSUDAPI.Services.OrderDetailsServices;
using NEGOSUDAPI.Models.Entities;

namespace NEGOSUDAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailsController : Controller
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailsController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orderDetails = await _orderDetailService.GetAllOrderDetailsAsync();
            return Ok(orderDetails);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var orderDetail = await _orderDetailService.GetOrderDetailByIdAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return Ok(orderDetail);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderDetail orderDetail)
        {
            await _orderDetailService.AddOrderDetailAsync(orderDetail);
            return CreatedAtAction(nameof(Details), new { id = orderDetail.OrderDetailID }, orderDetail);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderDetailID)
            {
                return BadRequest();
            }
            await _orderDetailService.UpdateOrderDetailAsync(orderDetail);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _orderDetailService.DeleteOrderDetailAsync(id);
            return NoContent();
        }
    }
}
