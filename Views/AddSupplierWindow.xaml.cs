using System.Windows;
using Negosud.Models.Entities;
using Negosud.Services;

namespace Negosud.Views
{
    public partial class AddSupplierWindow : Window
    {
        private readonly SupplierService _supplierService;
        private readonly Supplier _supplierToEdit;

        public AddSupplierWindow(SupplierService supplierService)
        {
            InitializeComponent();
            _supplierService = supplierService;
            _supplierToEdit = null;
        }

        public AddSupplierWindow(SupplierService supplierService, Supplier supplierToEdit)
        {
            InitializeComponent();
            _supplierService = supplierService;
            _supplierToEdit = supplierToEdit;

            txtCustomerName.Text = _supplierToEdit.Name;
            txtCustomerEmail.Text = _supplierToEdit.Email;
            txtCustomerPhone.Text = _supplierToEdit.ContactInfo;
        }

        private void btnSaveCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (_supplierToEdit == null) 
            {
                var newSupplier = new Supplier
                {
                    Name = txtCustomerName.Text,
                    Email = txtCustomerEmail.Text,
                    ContactInfo = txtCustomerPhone.Text
                };
                _supplierService.AddSupplier(newSupplier);
                MessageBox.Show("Fournisseur ajouté avec succès !");
            }
            else 
            {
                _supplierToEdit.Name = txtCustomerName.Text;
                _supplierToEdit.Email = txtCustomerEmail.Text;
                _supplierToEdit.ContactInfo = txtCustomerPhone.Text;
                _supplierService.UpdateSupplier(_supplierToEdit);
                MessageBox.Show("Fournisseur modifié avec succès !");
            }

            this.Close(); 
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }
    }
}
