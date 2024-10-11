using System;
using System.Globalization;
using System.Windows;
using Negosud.Models.Entities;
using Negosud.Services;

namespace Negosud.Views
{
    public partial class AddOrderWindow : Window
    {
        private readonly OrderService _orderService;
        private readonly Order _orderToEdit;

        public AddOrderWindow(OrderService orderService)
        {
            InitializeComponent();
            _orderService = orderService;
            _orderToEdit = null;
        }

        public AddOrderWindow(OrderService orderService, Order orderToEdit)
        {
            InitializeComponent();
            _orderService = orderService;
            _orderToEdit = orderToEdit;

            txtCustomerId.Text = _orderToEdit.CustomerID.ToString();
            dpOrderDate.SelectedDate = _orderToEdit.OrderDate;
            txtTotalAmount.Text = _orderToEdit.TotalAmount.ToString();
            txtOrderStatus.Text = _orderToEdit.OrderStatus;
        }

        private void btnSaveOrder_Click(object sender, RoutedEventArgs e)
        {
            if (_orderToEdit == null)
            {
                var newOrder = new Order
                {
                    CustomerID = int.Parse(txtCustomerId.Text),
                    OrderDate = dpOrderDate.SelectedDate ?? DateTime.Now,
                    TotalAmount = decimal.Parse(txtTotalAmount.Text, CultureInfo.InvariantCulture), 
                    OrderStatus = txtOrderStatus.Text
                };
                _orderService.AddOrder(newOrder);
                MessageBox.Show("Commande ajoutée avec succès !");
            }
            else
            {
                _orderToEdit.CustomerID = int.Parse(txtCustomerId.Text);
                _orderToEdit.OrderDate = dpOrderDate.SelectedDate ?? DateTime.Now;
                _orderToEdit.TotalAmount = decimal.Parse(txtTotalAmount.Text, CultureInfo.InvariantCulture);
                _orderToEdit.OrderStatus = txtOrderStatus.Text;
                _orderService.UpdateOrder(_orderToEdit);
                MessageBox.Show("Commande modifiée avec succès !");
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
