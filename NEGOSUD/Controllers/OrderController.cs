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
			var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel") ?? new OrderViewModel
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

            // Retrieve or Create the OrderViewModel from the session state
            var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel") ?? new OrderViewModel
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
                    ProductName = product.Name,
                    Quantity = StockQuantity,
                    Price = product.PricePerUnit
                });
            }

            // Update the total amount
            model.TotalAmount = model.OrderDetails.Sum(od => od.Price * od.Quantity);

            // Save updated OrderViewModel in the session state
            HttpContext.Session.Set("OrderViewModel", model);

            // Redirect back to the Create view to show or update the order
            return RedirectToAction(nameof(Create));
        }

        [HttpGet]
		[Authorize]
		public async Task<IActionResult> Cart()
		{
			//Retrieve the OrderViewModel from the session state
			var model = HttpContext.Session.Get<OrderViewModel>("OrderViewModel");

			if (model == null || model.OrderDetails.Count == 0)
			{
				return RedirectToAction(nameof(Create));
			}
			return View(model);
		}
	}
}
