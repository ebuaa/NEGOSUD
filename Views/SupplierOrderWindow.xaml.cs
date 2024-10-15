using System.Windows;
using System.Windows.Controls;
using Negosud.Models.Entities;
using Negosud.Services;

namespace Negosud.Views
{
    public partial class SupplierOrderWindow : Window
    {
        private readonly SupplierService _supplierService;
        private readonly ProductService _productService;
        private readonly SupplierOrderService _supplierOrderService;
        private readonly CustomerService _customerService;
        private readonly OrderService _orderService;
        private readonly CategoryService _categoryService;

        public SupplierOrderWindow(ProductService productService, CategoryService categoryService, SupplierService supplierService, CustomerService customerService, OrderService orderService, SupplierOrderService supplierOrderService)
        {
            InitializeComponent();
            _supplierOrderService = supplierOrderService;
            _supplierService = supplierService;
            _productService = productService;
            _customerService = customerService;
            _orderService = orderService;
            _categoryService = categoryService;
            LoadSupplierOrders();
        }

        private void LoadSupplierOrders()
        {
            lvSupplierOrders.ItemsSource = _supplierOrderService.GetAllSupplierOrders();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addSupplierOrderWindow = new AddSupplierOrderWindow(_supplierService, _productService, _supplierOrderService);
            addSupplierOrderWindow.ShowDialog();
            LoadSupplierOrders();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lvSupplierOrders.SelectedItem is SupplierOrder selectedOrder)
            {
                var editSupplierOrderWindow = new AddSupplierOrderWindow(_supplierService, _productService, _supplierOrderService,selectedOrder);
                editSupplierOrderWindow.ShowDialog();
                LoadSupplierOrders();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une commande fournisseur à modifier.");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvSupplierOrders.SelectedItem is SupplierOrder selectedOrder)
            {
                _supplierOrderService.DeleteSupplierOrder(selectedOrder.SupplierOrderID);
                LoadSupplierOrders();
                MessageBox.Show("Commande fournisseur supprimée avec succès !");
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une commande fournisseur à supprimer.");
            }
        }

        private void lvSupplierOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvSupplierOrders.SelectedItem is SupplierOrder selectedOrder)
            {
                MessageBox.Show($"Commande sélectionnée : {selectedOrder.SupplierOrderID}");
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
