using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NEGOSUDAPI.Services.CategoriesServices;
using NEGOSUDAPI.Models.Entities;


namespace NEGOSUDAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoriesService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var category = await _categoriesService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category category)
        {
            await _categoriesService.AddCategoryAsync(category);
            return CreatedAtAction(nameof(Details), new { id = category.CategoryID }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] Category category)
        {
            if (id != category.CategoryID)
            {
                return BadRequest();
            }
            await _categoriesService.UpdateCategoryAsync(category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _categoriesService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
