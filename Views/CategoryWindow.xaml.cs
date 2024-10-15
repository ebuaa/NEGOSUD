using System.Windows;
using Negosud.Services;
using Negosud.Models.Entities;

namespace Negosud.Views
{
    public partial class CategoryWindow : Window
    {
        private readonly CategoryService _categoryService;
        private readonly ProductService _productService;
        private readonly SupplierService _supplierService;
        private readonly CustomerService _customerService;
        private readonly OrderService _orderService;
        private SupplierOrderService _supplierOrderService;

        public CategoryWindow(ProductService productService,
                       CategoryService categoryService,
                       SupplierService supplierService,
                       CustomerService customerService,
                       OrderService orderService,
                       SupplierOrderService supplierOrderService)
        {
            InitializeComponent();
            _productService = productService;
            _categoryService = categoryService;
            _supplierService = supplierService;
            _customerService = customerService;
            _orderService = orderService;
            LoadCategories();
            _supplierOrderService = supplierOrderService;
        }

        private void LoadCategories()
        {
            lvCategories.ItemsSource = _categoryService.GetAllCategories();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addCategoryWindow = new AddCategoryWindow(_categoryService);
            addCategoryWindow.ShowDialog(); 
            LoadCategories();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lvCategories.SelectedItem is Category selectedCategory)
            {
                var editCategoryWindow = new AddCategoryWindow(_categoryService, selectedCategory);
                editCategoryWindow.ShowDialog();
                LoadCategories();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une catégorie à modifier.");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvCategories.SelectedItem is Category selectedCategory)
            {
                _categoryService.DeleteCategory(selectedCategory.CategoryID);
                LoadCategories();
                MessageBox.Show("Catégorie supprimée avec succès !");
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une catégorie à supprimer.");
            }
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow(_productService, _categoryService, _supplierService, _customerService, _orderService, _supplierOrderService);
            mainWindow.Show();
            this.Close();
        }
    }
}
