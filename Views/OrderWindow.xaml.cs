using System;
using System.Windows;
using System.Collections.Generic;
using Negosud.Models.Entities; 
using Negosud.Services; 

namespace Negosud.Views
{
    public partial class OrderWindow : Window
    {
        private readonly OrderService _orderService;

        public OrderWindow(OrderService orderService)
        {
            InitializeComponent();
            _orderService = orderService;
            LoadOrders();
        }

        private void LoadOrders()
        {
            var orders = _orderService.GetAllOrders();
            dgOrders.ItemsSource = orders; 
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var newOrder = new Order
            {
                OrderID = 0, 
                CustomerID = 1, 
                OrderDate = DateTime.Now,
                TotalAmount = 100.00m, 
                OrderStatus = "Pending" 
            };

            _orderService.AddOrder(newOrder);
            LoadOrders();
            MessageBox.Show("Order added successfully!");
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgOrders.SelectedItem is Order selectedOrder)
            {
                
                selectedOrder.CustomerID = 2; 
                selectedOrder.TotalAmount = 200.00m; 
                selectedOrder.OrderDate = DateTime.Now; 
                selectedOrder.OrderStatus = "Processed"; 

                _orderService.UpdateOrder(selectedOrder);
                LoadOrders();
                MessageBox.Show("Order updated successfully!");
            }
            else
            {
                MessageBox.Show("Please select an order to edit.");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgOrders.SelectedItem is Order selectedOrder)
            {
                _orderService.DeleteOrder(selectedOrder); 
                LoadOrders();
                MessageBox.Show("Order deleted successfully!");
            }
            else
            {
                MessageBox.Show("Please select an order to delete.");
            }
        }


        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dgOrders_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgOrders.SelectedItem is Order selectedOrder)
            {
                // Populate any input fields if needed for the selected order
                // txtCustomerId.Text = selectedOrder.CustomerID.ToString();
                //  txtTotalAmount.Text = selectedOrder.TotalAmount.ToString();
                //  dpOrderDate.SelectedDate = selectedOrder.OrderDate;
            }
        }
    }
}
