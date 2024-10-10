using Negosud.Data;
using Negosud.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Negosud.Services
{
    public class ProductService
    {
        private readonly NegosudContext _context;
        private CategoryService categoryService;
        private SupplierService supplierService;

        public ProductService(NegosudContext context)
        {
            _context = context;
        }

        public ProductService(NegosudContext context, CategoryService categoryService, SupplierService supplierService) : this(context)
        {
            this.categoryService = categoryService;
            this.supplierService = supplierService;
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Find(id);
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}
