using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Negosud.Models.Entities;
using Negosud.Services;
using Microsoft.Win32;

namespace Negosud.Views
{
    public partial class AddProductWindow : Window
    {
        private ProductService _productService;
        private CategoryService _categoryService;
        private SupplierService _supplierService;

        public ProductService ProductService { get; }

        public AddProductWindow(ProductService productService, CategoryService categoryService, SupplierService supplierService)
        {
            InitializeComponent();
            _productService = productService;
            _categoryService = categoryService;
            _supplierService = supplierService;
            LoadCategories();
            LoadSuppliers();
        }

        public AddProductWindow(ProductService productService)
        {
            ProductService = productService;
        }

        private void LoadCategories()
        {
            var categories = _categoryService.GetAllCategories(); 
            cmbCategories.ItemsSource = categories;
            cmbCategories.DisplayMemberPath = "Name"; 
            cmbCategories.SelectedValuePath = "CategoryID"; 
        }

        private void LoadSuppliers()
        {
            var suppliers = _supplierService.GetAllSuppliers(); 
            cmbSuppliers.ItemsSource = suppliers;
            cmbSuppliers.DisplayMemberPath = "Name"; 
            cmbSuppliers.SelectedValuePath = "SupplierID"; 
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cmbCategories.SelectedValue == null || cmbSuppliers.SelectedValue == null)
            {
                MessageBox.Show("Veuillez sélectionner une catégorie et un fournisseur !");
                return;
            }

            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var newProduct = new Product
                {
                    Name = txtName.Text,
                    PricePerUnit = decimal.Parse(txtPrice.Text),
                    StockQuantity = int.Parse(txtStock.Text),
                    MinimumStock = int.Parse(txtMinimumStock.Text),
                    CategoryID = (int)cmbCategories.SelectedValue, 
                    SupplierID = (int)cmbSuppliers.SelectedValue, 
                    ImageUrl = openFileDialog.FileName 
                };

                _productService.AddProduct(newProduct); 
                MessageBox.Show("Produit ajouté avec succès !");
                this.Close();
            }
        }
    }
}
