using Negosud.Models.Entities;
using Negosud.Services;
using System.Windows;

namespace Negosud.Views
{
    public partial class ProductWindow : Window
    {
        private ProductService _productService;

        public ProductWindow(ProductService productService)
        {
            InitializeComponent();
            _productService = productService;
            LoadProducts();
        }

        private void LoadProducts()
        {
            lvProducts.ItemsSource = _productService.GetAllProducts();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addProductWindow = new AddProductWindow(_productService);
            addProductWindow.ShowDialog();
            LoadProducts();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lvProducts.SelectedItem is Product selectedProduct)
            {
                var editProductWindow = new EditProductWindow(_productService, selectedProduct);
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
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un produit à supprimer.");
            }
        }

        private void lvProducts_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Gérer un changement de sélection dans la liste, si nécessaire
        }
    }
}
