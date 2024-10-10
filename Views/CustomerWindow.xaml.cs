using Negosud.Models.Entities;
using Negosud.Services;
using System.Windows;

namespace Negosud.Views
{
    public partial class CustomerWindow : Window
    {
        private CustomerService _customerService;

        public CustomerWindow(CustomerService customerService)
        {
            InitializeComponent();
            _customerService = customerService;
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            lvCustomers.ItemsSource = _customerService.GetAllCustomers();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Customer added successfully!");
            LoadCustomers();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvCustomers.SelectedItem is Customer selectedCustomer)
            {
                _customerService.DeleteCustomer(selectedCustomer.CustomerID);
                MessageBox.Show("Customer deleted successfully!");
                LoadCustomers();
            }
            else
            {
                MessageBox.Show("Please select a customer to delete.");
            }
        }
    }
}
