using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NEGOSUD.Data;
using NEGOSUD.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NEGOSUD.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Customer> _userManager;

        public OrderController(ApplicationDbContext context, UserManager<Customer> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Get the current customer's ID
            var customerId = _userManager.GetUserId(User);
            if (customerId == null)
            {
                return Unauthorized();
            }

            // Create a customer-specific session key
            var sessionKey = $"OrderViewModel_{customerId}";

            var model = HttpContext.Session.Get<OrderViewModel>(sessionKey) ?? new OrderViewModel
            {
                OrderDetails = new List<OrderDetailViewModel>(),
                Products = await _context.Products.ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddDetail(int productID, int StockQuantity)
        {
            var product = await _context.Products.FindAsync(productID);
            if (product == null)
            {
                return NotFound();
            }

            // Get the current customer's ID
            var customerId = _userManager.GetUserId(User);
            if (customerId == null)
            {
                return Unauthorized();
            }

            // Create a customer-specific session key
            var sessionKey = $"OrderViewModel_{customerId}";

            // Retrieve or Create the OrderViewModel from the session state
            var model = HttpContext.Session.Get<OrderViewModel>(sessionKey) ?? new OrderViewModel
            {
                OrderDetails = new List<OrderDetailViewModel>(),
                Products = await _context.Products.ToListAsync()
            };

            // Check if the product is already in the order
            var existingDetail = model.OrderDetails.Find(od => od.ProductID == productID);

            // If the product is already in the order, update the quantity
            if (existingDetail != null)
            {
                existingDetail.Quantity += StockQuantity;
            }
            else
            {
                model.OrderDetails.Add(new OrderDetailViewModel
                {
                    ProductID = productID,
                    ProductName = product.Name ?? "Unknown Product",
                    Quantity = StockQuantity,
                    Price = product.PricePerUnit
                });
            }

            // Update the total amount
            model.TotalAmount = model.OrderDetails.Sum(od => od.Price * od.Quantity);

            // Save updated OrderViewModel in the session state
            HttpContext.Session.Set(sessionKey, model);

            // Redirect back to the Create view to show or update the order
            return RedirectToAction(nameof(Create));
        }

        [HttpGet]
        [Authorize]
        public IActionResult Cart()
        {
            // Get the current customer's ID
            var customerId = _userManager.GetUserId(User);
            if (customerId == null)
            {
                return Unauthorized();
            }

            // Create a customer-specific session key
            var sessionKey = $"OrderViewModel_{customerId}";

            // Retrieve the OrderViewModel from the session state
            var model = HttpContext.Session.Get<OrderViewModel>(sessionKey);

            if (model == null || model.OrderDetails.Count == 0)
            {
                return RedirectToAction(nameof(Create));
            }
            return View(model);
        }
    }
}
