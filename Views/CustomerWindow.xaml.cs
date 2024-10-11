using System.Windows;
using Negosud.Models.Entities;
using Negosud.Services;

namespace Negosud.Views
{
    public partial class CustomerWindow : Window
    {
        private readonly CustomerService _customerService;
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly SupplierService _supplierService;
        private readonly OrderService _orderService;

        public CustomerWindow(ProductService productService,
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
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            lvCustomers.ItemsSource = _customerService.GetAllCustomers();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addCustomerWindow = new AddCustomerWindow(_customerService);
            addCustomerWindow.ShowDialog();
            LoadCustomers();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lvCustomers.SelectedItem is Customer selectedCustomer)
            {
                var editCustomerWindow = new AddCustomerWindow(_customerService, selectedCustomer);
                editCustomerWindow.ShowDialog();
                LoadCustomers();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un client à modifier.");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvCustomers.SelectedItem is Customer selectedCustomer)
            {
                _customerService.DeleteCustomer(selectedCustomer.CustomerID);
                LoadCustomers();
                MessageBox.Show("Client supprimé avec succès !");
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un client à supprimer.");
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
