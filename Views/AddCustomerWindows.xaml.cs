using System;
using System.Windows;
using Negosud.Models.Entities;
using Negosud.Services;

namespace Negosud.Views
{
    public partial class AddCustomerWindow : Window
    {
        private readonly CustomerService _customerService;
        private readonly Customer _customerToEdit;

        public AddCustomerWindow(CustomerService customerService)
        {
            InitializeComponent();
            _customerService = customerService;
        }

        public AddCustomerWindow(CustomerService customerService, Customer customerToEdit)
        {
            InitializeComponent();
            _customerService = customerService;
            _customerToEdit = customerToEdit;

            txtCustomerName.Text = _customerToEdit.Name;
            txtCustomerEmail.Text = _customerToEdit.Email;
            txtCustomerPhone.Text = _customerToEdit.Phone;
            txtCustomerAddress.Text = _customerToEdit.Address;
            dpCustomerDOB.SelectedDate = _customerToEdit.DateOfBirth;
        }

        private void btnSaveCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (_customerToEdit == null)
            {
                var newCustomer = new Customer
                {
                    Name = txtCustomerName.Text,
                    Email = txtCustomerEmail.Text,
                    Phone = txtCustomerPhone.Text,
                    Address = txtCustomerAddress.Text,
                    DateOfBirth = dpCustomerDOB.SelectedDate ?? DateTime.Now,
                    Password = txtCustomerPassword.Password
                };

                _customerService.AddCustomer(newCustomer);
                MessageBox.Show("Client ajouté avec succès !");
            }
            else
            {
                _customerToEdit.Name = txtCustomerName.Text;
                _customerToEdit.Email = txtCustomerEmail.Text;
                _customerToEdit.Phone = txtCustomerPhone.Text;
                _customerToEdit.Address = txtCustomerAddress.Text;
                _customerToEdit.DateOfBirth = dpCustomerDOB.SelectedDate ?? DateTime.Now;

                _customerService.UpdateCustomer(_customerToEdit);
                MessageBox.Show("Client modifié avec succès !");
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
