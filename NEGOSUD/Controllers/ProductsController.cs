using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NEGOSUD.Data;

namespace NEGOSUD.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
          .Include(p => p.Category) // Include related category data if needed
          .Include(p => p.Supplier) // Include related supplier data if needed
          .ToListAsync();
            return View(products);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _context.Products
          .Include(p => p.Category) // Include related category data if needed
          .Include(p => p.Supplier) // Include related supplier data if needed
          .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
