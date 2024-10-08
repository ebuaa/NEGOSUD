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
	}
}
