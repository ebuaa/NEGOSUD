using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Negosud.Models;
using Negosud.Models.Entities;
using Negosud.Services;
using Negosud.Data;


namespace Negosud.Views
{
    public partial class EditProductWindow : Window
    {
        private ProductService _productService;
        private Product _product;

        public EditProductWindow(ProductService productService, Product product)
        {
            InitializeComponent();
            _productService = productService;
            _product = product;
            LoadProductData();
        }

        private void LoadProductData()
        {
            txtName.Text = _product.Name;
            txtDescription.Text = _product.Description;
            txtPrice.Text = _product.PricePerUnit.ToString();
            txtStock.Text = _product.StockQuantity.ToString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _product.Name = txtName.Text;
            _product.Description = txtDescription.Text;
            _product.PricePerUnit = decimal.Parse(txtPrice.Text);
            _product.StockQuantity = int.Parse(txtStock.Text);

            _productService.UpdateProduct(_product);
            this.Close();
        }
    }
}
