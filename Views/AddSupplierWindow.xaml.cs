using System;
using System.Windows;
using System.Windows.Controls;
using Negosud.Models.Entities;
using Negosud.Services;

namespace Negosud.Views
{
    public partial class AddSupplierWindow : Window
    {
        private SupplierService _supplierService;

        public AddSupplierWindow(SupplierService supplierService)
        {
            InitializeComponent();
            _supplierService = supplierService;
            LoadSuppliers();
        }

        private void LoadSuppliers()
        {
            var suppliers = _supplierService.GetAllSuppliers();
            lstSuppliers.ItemsSource = suppliers;
        }

        private void btnAddSupplier_Click(object sender, RoutedEventArgs e)
        {
            var newSupplier = new Supplier
            {
                Name = txtSupplierName.Text,
            };

            _supplierService.AddSupplier(newSupplier);
            LoadSuppliers();
            MessageBox.Show("Fournisseur ajouté avec succès !");
        }

        private void btnDeleteSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (lstSuppliers.SelectedItem is Supplier selectedSupplier)
            {
                _supplierService.DeleteSupplier(selectedSupplier.SupplierID); 
                LoadSuppliers();
                MessageBox.Show("Fournisseur supprimé avec succès !");
            }
        }

        private void btnUpdateSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (lstSuppliers.SelectedItem is Supplier selectedSupplier)
            {
                selectedSupplier.Name = txtSupplierName.Text;
                _supplierService.UpdateSupplier(selectedSupplier);
                LoadSuppliers();
                MessageBox.Show("Fournisseur mis à jour avec succès !");
            }
        }

        private void lstSuppliers_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstSuppliers.SelectedItem is Supplier selectedSupplier)
            {
                txtSupplierName.Text = selectedSupplier.Name; 
            }
        }
    }
}
