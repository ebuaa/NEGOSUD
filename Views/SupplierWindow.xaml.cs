using System.Windows;
using Negosud.Services;
using Negosud.Models.Entities;

namespace Negosud.Views
{
    public partial class SupplierWindow : Window
    {
        private readonly CustomerService _customerService;
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly SupplierService _supplierService;
        private readonly OrderService _orderService;

        public SupplierWindow(ProductService productService,
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
            LoadSuppliers();
        }

        private void LoadSuppliers()
        {
            lvSuppliers.ItemsSource = _supplierService.GetAllSuppliers();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addSupplierWindow = new AddSupplierWindow(_supplierService);
            addSupplierWindow.ShowDialog();
            LoadSuppliers();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lvSuppliers.SelectedItem is Supplier selectedSupplier)
            {
                var editSupplierWindow = new AddSupplierWindow(_supplierService, selectedSupplier);
                editSupplierWindow.ShowDialog();
                LoadSuppliers();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un fournisseur à modifier.");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvSuppliers.SelectedItem is Supplier selectedSupplier)
            {
                _supplierService.DeleteSupplier(selectedSupplier.SupplierID);
                LoadSuppliers();
                MessageBox.Show("Fournisseur supprimé avec succès !");
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un fournisseur à supprimer.");
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
