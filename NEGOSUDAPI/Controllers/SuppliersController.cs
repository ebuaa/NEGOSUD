using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NEGOSUDAPI.Services.SuppliersServices;
using NEGOSUDAPI.Models.Entities;

namespace NEGOSUDAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : Controller
    {
        private readonly ISuppliersService _suppliersService;

        public SuppliersController(ISuppliersService suppliersService)
        {
            _suppliersService = suppliersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var suppliers = await _suppliersService.GetAllSuppliersAsync();
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var supplier = await _suppliersService.GetSupplierByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return Ok(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Supplier supplier)
        {
            await _suppliersService.AddSupplierAsync(supplier);
            return CreatedAtAction(nameof(Details), new { id = supplier.SupplierID }, supplier);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] Supplier supplier)
        {
            if (id != supplier.SupplierID)
            {
                return BadRequest();
            }
            await _suppliersService.UpdateSupplierAsync(supplier);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _suppliersService.DeleteSupplierAsync(id);
            return NoContent();
        }
    }
}
