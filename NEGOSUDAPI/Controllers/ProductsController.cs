using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NEGOSUDAPI.Services.ProductsServices;
using NEGOSUDAPI.Models.Entities;

namespace NEGOSUDAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productsService.GetAllProductsAsync();
            return Ok(products);
           
        }   

        [HttpGet("{id}")]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var product = await _productsService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            await _productsService.AddProductAsync(product);
            return CreatedAtAction(nameof(Details), new { id = product.ProductID }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] Product product)
        {
            if (id != product.ProductID)
            {
                return BadRequest();
            }
            await _productsService.UpdateProductAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _productsService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}