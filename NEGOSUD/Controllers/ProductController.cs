using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NEGOSUD.Data;
using NEGOSUD.Models.Entities;

namespace NEGOSUD.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(string? search, string? category, string? sortProduct)
        {
            //Basic query
            var productsQuery = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .AsQueryable();

            //Search
            if (!string.IsNullOrEmpty(search))
            {
                productsQuery = productsQuery.Where(p => p.Name.Contains(search) || p.Description.Contains(search) || p.Category.Name.Contains(search));
            }

            //Category filter 
            if (!string.IsNullOrEmpty(category))
            {
                productsQuery = productsQuery.Where(p => p.Category.Name == category);
            }

            //Sort
            switch (sortProduct)
            {
                case "name_asc":
                    productsQuery = productsQuery.OrderBy(p => p.Name);
                    break;
                case "name_desc":
                    productsQuery = productsQuery.OrderByDescending(p => p.Name);
                    break;
                case "price_asc":
                    productsQuery = productsQuery.OrderBy(p => p.PricePerUnit);
                    break;
                case "price_desc":
                    productsQuery = productsQuery.OrderByDescending(p => p.PricePerUnit);
                    break;
                case "year_asc":
                    productsQuery = productsQuery.OrderBy(p => p.Year);
                    break;
                case "year_desc":
                    productsQuery = productsQuery.OrderByDescending(p => p.Year);
                    break;
                default:
                    productsQuery = productsQuery.OrderBy(p => p.Name); // Default sorting
                    break;
            }


            //execute query
            var products = await productsQuery.ToListAsync();

            //return view
            ViewBag.Search = search;
            ViewBag.Category = category;
            ViewBag.SortProduct = sortProduct;
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}