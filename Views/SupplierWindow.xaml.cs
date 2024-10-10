using Negosud.Models.Entities;
using Negosud.Services;
using System.Windows;

namespace Negosud.Views
{
    public partial class SupplierWindow : Window
    {
        private SupplierService _supplierService;

        public SupplierWindow(SupplierService supplierService)
        {
            InitializeComponent();
            _supplierService = supplierService;
            LoadSuppliers();
        }

        private void LoadSuppliers()
        {
            lvSuppliers.ItemsSource = _supplierService.GetAllSuppliers();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Add supplier logic here
            MessageBox.Show("Supplier added successfully!");
            LoadSuppliers();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvSuppliers.SelectedItem is Supplier selectedSupplier)
            {
                _supplierService.DeleteSupplier(selectedSupplier.SupplierID);
                MessageBox.Show("Supplier deleted successfully!");
                LoadSuppliers();
            }
            else
            {
                MessageBox.Show("Please select a supplier to delete.");
            }
        }
    }
}
