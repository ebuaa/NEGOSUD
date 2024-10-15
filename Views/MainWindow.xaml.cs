using System.Windows;
using Negosud.Data;
using Negosud.Services;

namespace Negosud.Views
{
    public partial class MainWindow : Window
    {
        private ProductService _productService;
        private CategoryService _categoryService;
        private SupplierService _supplierService;
        private CustomerService _customerService;
        private OrderService _orderService;
        private SupplierOrderService _supplierOrderService;
        private NegosudContext _context;

        public MainWindow()
        {
            InitializeComponent();
            InitializeServices();
        }

        public MainWindow(ProductService productService, CategoryService categoryService, SupplierService supplierService, CustomerService customerService, OrderService orderService, SupplierOrderService supplierOrderService)
        {
            InitializeComponent();
            _productService = productService;
            _categoryService = categoryService;
            _supplierService = supplierService;
            _customerService = customerService;
            _orderService = orderService;
            _supplierOrderService = supplierOrderService;
        }

        private void InitializeServices()
        {
            _context = new NegosudContext();
            _productService = new ProductService(_context);
            _categoryService = new CategoryService(_context);
            _supplierService = new SupplierService(_context);
            _customerService = new CustomerService(_context);
            _orderService = new OrderService(_context);
            _supplierOrderService = new SupplierOrderService(_context);
        }

        private void btnManageProducts_Click(object sender, RoutedEventArgs e)
        {
            var productWindow = new ProductWindow(_productService, _categoryService, _supplierService, _customerService, _orderService, _supplierOrderService);
            productWindow.Show();
            this.Hide();
        }

        private void btnManageCategories_Click(object sender, RoutedEventArgs e)
        {
            var categoryWindow = new CategoryWindow(_productService, _categoryService, _supplierService, _customerService, _orderService, _supplierOrderService);
            categoryWindow.Show();
            this.Hide();
        }

        private void btnManageCustomers_Click(object sender, RoutedEventArgs e)
        {
            var customerWindow = new CustomerWindow(_productService, _categoryService, _supplierService, _customerService, _orderService, _supplierOrderService);
            customerWindow.Show();
            this.Hide();
        }

        private void btnManageSuppliers_Click(object sender, RoutedEventArgs e)
        {
            var supplierWindow = new SupplierWindow(_productService, _categoryService, _supplierService, _customerService, _orderService, _supplierOrderService);
            supplierWindow.Show();
            this.Hide();
        }

        private void btnManageOrder_Click(object sender, RoutedEventArgs e)
        {
            var orderWindow = new OrderWindow(_productService, _categoryService, _supplierService, _customerService, _orderService, _supplierOrderService);
            orderWindow.Show();
            this.Hide();
        }

        private void btnManageSupplierOrders_Click(object sender, RoutedEventArgs e)
        {
            var supplierOrderWindow = new SupplierOrderWindow(_productService, _categoryService, _supplierService, _customerService, _orderService, _supplierOrderService);
            supplierOrderWindow.Show();
            this.Hide();
        }

        public void ShowMainWindow()
        {
            this.Show();
        }
    }
}
