using System.Windows;
using System.Windows.Controls;
using Negosud.Models.Entities;
using Negosud.Services;

namespace Negosud.Views
{
    public partial class ProductWindow : Window
    {
        private readonly CustomerService _customerService;
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly SupplierService _supplierService;
        private readonly OrderService _orderService;

        public ProductWindow(ProductService productService,
                           CategoryService categoryService,
                           SupplierService supplierService,
                           CustomerService customerService,
                           OrderService orderService)
        {
            InitializeComponent();
            _productService = productService;
            _categoryService = categoryService;
            _supplierService = supplierService;
            _customerService = customerService;
            _orderService = orderService;
            LoadProducts();
        }
        private void LoadProducts()
        {
           
            lvProducts.ItemsSource = _productService.GetAllProductsWithDetails(); 
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addProductWindow = new AddProductWindow(_productService, _categoryService, _supplierService);
            addProductWindow.ShowDialog(); 
            LoadProducts();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lvProducts.SelectedItem is Product selectedProduct)
            {
                var editProductWindow = new AddProductWindow(_productService, _categoryService, _supplierService, selectedProduct);
                editProductWindow.ShowDialog(); 
                LoadProducts(); 
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un produit à modifier.");
            }
        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvProducts.SelectedItem is Product selectedProduct)
            {
                _productService.DeleteProduct(selectedProduct.ProductID);
                LoadProducts(); 
                MessageBox.Show("Produit supprimé avec succès !");
            }
            else
            {
                MessageBox.Show("Please select a product to delete.");
            }
        }
        private void lvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
     
            if (lvProducts.SelectedItem is Product selectedProduct)
            {
                MessageBox.Show($"Produit sélectionné : {selectedProduct.Name}");
            }
        }


        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow(_productService, _categoryService, _supplierService, _customerService, _orderService);
            mainWindow.Show();
            this.Close();
        }
    }
}
