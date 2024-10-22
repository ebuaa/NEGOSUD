using System;
using System.Linq;
using System.Windows;
using Negosud.Models.Entities;
using Negosud.Services;

namespace Negosud.Views
{
    public partial class AddSupplierOrderWindow : Window
    {
        private readonly SupplierService _supplierService;
        private readonly ProductService _productService;
        private readonly SupplierOrderService _supplierOrderService;
        private SupplierOrder _selectedOrder;

        public AddSupplierOrderWindow(SupplierService supplierService, ProductService productService, SupplierOrderService supplierOrderService)
        {
            InitializeComponent();
            _supplierService = supplierService;
            _productService = productService;
            _supplierOrderService = supplierOrderService;
            LoadSuppliers();
            LoadProducts();
        }

        public AddSupplierOrderWindow(SupplierService supplierService, ProductService productService, SupplierOrderService supplierOrderService, SupplierOrder selectedOrder)
            : this(supplierService, productService, supplierOrderService)
        {
            _selectedOrder = selectedOrder;
            LoadOrderData();
        }

        private void LoadSuppliers()
        {
            var suppliers = _supplierService.GetAllSuppliers();
            cmbSuppliers.ItemsSource = suppliers;
            cmbSuppliers.DisplayMemberPath = "Name";
            cmbSuppliers.SelectedValuePath = "SupplierID";
        }

        private void LoadProducts()
        {
            var products = _productService.GetAllProducts();
            cmbProducts.ItemsSource = products;
            cmbProducts.DisplayMemberPath = "Name";
            cmbProducts.SelectedValuePath = "ProductID";
        }

        private void LoadOrderData()
        {
            if (_selectedOrder != null)
            {
                cmbSuppliers.SelectedValue = _selectedOrder.SupplierID;
                dpOrderDate.SelectedDate = _selectedOrder.OrderDate;
                txtTotalAmount.Text = _selectedOrder.TotalAmount.ToString();

                var orderDetail = _selectedOrder.SupplierOrderDetails.FirstOrDefault();
                if (orderDetail != null)
                {
                    cmbProducts.SelectedValue = orderDetail.ProductID;
                    txtQuantity.Text = orderDetail.Quantity.ToString();
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedSupplier = cmbSuppliers.SelectedItem as Supplier;
                var selectedProduct = cmbProducts.SelectedItem as Product;

                if (selectedSupplier == null)
                {
                    MessageBox.Show("Veuillez sélectionner un fournisseur.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (selectedProduct == null)
                {
                    MessageBox.Show("Veuillez sélectionner un produit.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                DateTime orderDate = dpOrderDate.SelectedDate.HasValue ? dpOrderDate.SelectedDate.Value : DateTime.Now;

                if (!decimal.TryParse(txtTotalAmount.Text, out decimal totalAmount))
                {
                    MessageBox.Show("Veuillez entrer un montant total valide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!int.TryParse(txtQuantity.Text, out int quantity))
                {
                    MessageBox.Show("Veuillez entrer une quantité valide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (_selectedOrder == null) 
                {
                    var newOrder = new SupplierOrder
                    {
                        SupplierID = selectedSupplier.SupplierID,
                        OrderDate = orderDate,
                        TotalAmount = totalAmount
                    };

                    var orderDetail = new SupplierOrderDetail
                    {
                        ProductID = selectedProduct.ProductID,
                        Quantity = quantity
                    };
                    newOrder.SupplierOrderDetails.Add(orderDetail);

                    _supplierOrderService.AddSupplierOrder(newOrder);

                    selectedProduct.StockQuantity += quantity;
                    _productService.UpdateProduct(selectedProduct);

                    MessageBox.Show("Commande fournisseur ajoutée avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else 
                {
                    _selectedOrder.SupplierID = selectedSupplier.SupplierID;
                    _selectedOrder.OrderDate = orderDate;
                    _selectedOrder.TotalAmount = totalAmount;

                    var orderDetail = _selectedOrder.SupplierOrderDetails.FirstOrDefault();
                    if (orderDetail != null)
                    {
                        // Mise à jour des détails existants
                        orderDetail.ProductID = selectedProduct.ProductID;
                        orderDetail.Quantity = quantity;
                    }
                    else
                    {
                        // Ajout d'un nouveau détail si aucun n'existe
                        _selectedOrder.SupplierOrderDetails.Add(new SupplierOrderDetail
                        {
                            ProductID = selectedProduct.ProductID,
                            Quantity = quantity
                        });
                    }

                    _supplierOrderService.UpdateSupplierOrder(_selectedOrder);

                    // Mise à jour du stock
                    selectedProduct.StockQuantity += quantity;
                    _productService.UpdateProduct(selectedProduct);

                    MessageBox.Show("Commande fournisseur modifiée avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'enregistrement de la commande : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
