using Microsoft.EntityFrameworkCore;
using NEGOSUDAPI.Data;
using NEGOSUDAPI.Models.Entities;
using NEGOSUDAPI.Services.OrderDetailsServices;

namespace NEGOSUDAPI.Services.OrderDetailsServices
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync()
        {
            return await _context.OrderDetails
                .Include(od => od.Order)
                .Include(od => od.Product)
                .ToListAsync();
        }

        public async Task<OrderDetail?> GetOrderDetailByIdAsync(int orderDetailId)
        {
            return await _context.OrderDetails
                .Include(od => od.Order)
                .Include(od => od.Product)
                .FirstOrDefaultAsync(od => od.OrderDetailID == orderDetailId);
        }

        public async Task AddOrderDetailAsync(OrderDetail orderDetail)
        {
            var orderExists = await _context.Orders.AnyAsync(o => o.OrderID == orderDetail.OrderID);
            if (!orderExists)
            {
                throw new Exception($"La commande avec l'ID {orderDetail.OrderID} n'existe pas.");
            }

            var productExists = await _context.Products.AnyAsync(p => p.ProductID == orderDetail.ProductID);
            if (!productExists)
            {
                throw new Exception($"Le produit avec l'ID {orderDetail.ProductID} n'existe pas.");
            }

            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderDetailAsync(int orderDetailId)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(orderDetailId);
            if (orderDetail == null)
            {
                throw new Exception($"Le détail de commande avec l'ID {orderDetailId} n'existe pas.");
            }

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();
        }
    }
}
