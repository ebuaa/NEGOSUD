using System.Windows;
using Negosud.Models.Entities;
using Negosud.Services;

namespace Negosud.Views
{
    public partial class OrderWindow : Window
    {
        private readonly CustomerService _customerService;
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly SupplierService _supplierService;
        private readonly OrderService _orderService;

        public OrderWindow(ProductService productService,
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
            LoadOrders();
        }

        private void LoadOrders()
        {
            lvOrders.ItemsSource = _orderService.GetAllOrders();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addOrderWindow = new AddOrderWindow(_orderService);
            addOrderWindow.ShowDialog();
            LoadOrders(); 
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lvOrders.SelectedItem is Order selectedOrder)
            {
                var editOrderWindow = new AddOrderWindow(_orderService, selectedOrder);
                editOrderWindow.ShowDialog(); 
                LoadOrders(); 
            }
            else
            {
                MessageBox.Show("Please select an order to edit.");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvOrders.SelectedItem is Order selectedOrder)
            {
                
                var orderToDelete = _orderService.GetOrderById(selectedOrder.OrderID);

                if (orderToDelete != null)
                {
                    _orderService.DeleteOrder(orderToDelete); 
                    LoadOrders(); 
                    MessageBox.Show("Order deleted successfully!");
                }
                else
                {
                    MessageBox.Show("Order not found.");
                }
            }
            else
            {
                MessageBox.Show("Please select an order to delete.");
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
