using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Negosud.Models.Entities;
using Negosud.Services;

namespace Negosud.ViewModels
{
    public class OrderViewModel
    {
        public ObservableCollection<Order> Orders { get; set; }

        private readonly OrderService _orderService;

        public OrderViewModel(OrderService orderService)
        {
            _orderService = orderService;
            LoadOrders();
        }

        public void LoadOrders()
        {
            var orders = _orderService.GetAllOrders();
            Orders = new ObservableCollection<Order>(orders);
        }
    }
}
