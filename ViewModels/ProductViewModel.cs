using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Negosud.Models.Entities;
using Negosud.Services;

namespace Negosud.ViewModels
{
    public class ProductViewModel
    {
        public ObservableCollection<Product> Products { get; set; }

        private readonly ProductService _productService;

        public ProductViewModel(ProductService productService)
        {
            _productService = productService;
            LoadProducts();
        }

        public void LoadProducts()
        {
            var products = _productService.GetAllProducts();
            Products = new ObservableCollection<Product>(products);
        }
    }
}
