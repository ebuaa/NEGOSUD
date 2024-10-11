using System;
using System.Globalization;
using System.Windows;
using Microsoft.Win32; 
using Negosud.Models.Entities;
using Negosud.Services;

namespace Negosud.Views
{
    public partial class AddProductWindow : Window
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly SupplierService _supplierService;
        private readonly Product _productToEdit;

        public AddProductWindow(ProductService productService, CategoryService categoryService, SupplierService supplierService)
        {
            InitializeComponent();
            _productService = productService;
            _categoryService = categoryService;
            _supplierService = supplierService;
            _productToEdit = null;

            LoadCategoriesAndSuppliers();
        }

        public AddProductWindow(ProductService productService, CategoryService categoryService, SupplierService supplierService, Product productToEdit)
            : this(productService, categoryService, supplierService)
        {
            _productToEdit = productToEdit;

            txtName.Text = _productToEdit.Name;
            txtDescription.Text = _productToEdit.Description;
            txtPrice.Text = _productToEdit.PricePerUnit.ToString(CultureInfo.InvariantCulture);
            txtStock.Text = _productToEdit.StockQuantity.ToString();
            txtMinimumStock.Text = _productToEdit.MinimumStock.ToString();
            cmbCategories.SelectedValue = _productToEdit.CategoryID;
            cmbSuppliers.SelectedValue = _productToEdit.SupplierID;
            txtImageUrl.Text = _productToEdit.ImageUrl; 
        }

        private void LoadCategoriesAndSuppliers()
        {
            cmbCategories.ItemsSource = _categoryService.GetAllCategories();
            cmbCategories.DisplayMemberPath = "Name";
            cmbCategories.SelectedValuePath = "CategoryID";

            cmbSuppliers.ItemsSource = _supplierService.GetAllSuppliers();
            cmbSuppliers.DisplayMemberPath = "Name";
            cmbSuppliers.SelectedValuePath = "SupplierID";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_productToEdit == null)
            {
                var newProduct = new Product
                {
                    Name = txtName.Text,
                    Description = txtDescription.Text,
                    PricePerUnit = decimal.Parse(txtPrice.Text, CultureInfo.InvariantCulture),
                    StockQuantity = int.Parse(txtStock.Text),
                    MinimumStock = int.Parse(txtMinimumStock.Text),
                    CategoryID = (int)cmbCategories.SelectedValue,
                    SupplierID = (int)cmbSuppliers.SelectedValue,
                    ImageUrl = txtImageUrl.Text
                };

                _productService.AddProduct(newProduct);
                MessageBox.Show("Produit ajouté avec succès !");
            }
            else 
            {
                var selectedCategory = _categoryService.GetCategoryById((int)cmbCategories.SelectedValue);
                var selectedSupplier = _supplierService.GetSupplierById((int)cmbSuppliers.SelectedValue);

                _productToEdit.Name = txtName.Text;
                _productToEdit.Description = txtDescription.Text;
                _productToEdit.PricePerUnit = decimal.Parse(txtPrice.Text, CultureInfo.InvariantCulture);
                _productToEdit.StockQuantity = int.Parse(txtStock.Text);
                _productToEdit.MinimumStock = int.Parse(txtMinimumStock.Text);
                _productToEdit.Category = selectedCategory; 
                _productToEdit.Supplier = selectedSupplier; 
                _productToEdit.ImageUrl = txtImageUrl.Text;

                _productService.UpdateProduct(_productToEdit);
                MessageBox.Show("Produit modifié avec succès !");
            }

            this.Close();
        }


        private void btnBrowseImage_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Select an Image"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                txtImageUrl.Text = openFileDialog.FileName;
            }
        }
    }
}
