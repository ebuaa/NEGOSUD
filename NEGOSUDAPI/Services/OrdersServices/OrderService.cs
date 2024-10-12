using Microsoft.EntityFrameworkCore;
using NEGOSUDAPI.Data;
using NEGOSUDAPI.Models.Entities;
using NEGOSUDAPI.Services.OrdersServices;


namespace NEGOSUDAPI.Services.OrdersServices
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderID == orderId);
        }

        public async Task AddOrderAsync(Order order)
        {
            if (int.TryParse(order.CustomerID, out var customerId))
            {
                var customerExists = await _context.Customers.AnyAsync(c => c.CustomerID == customerId);
                if (!customerExists)
                {
                    throw new Exception($"Le client avec l'ID {order.CustomerID} n'existe pas.");
                }
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new Exception($"La commande avec l'ID {orderId} n'existe pas.");
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
