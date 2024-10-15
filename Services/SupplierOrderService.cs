using System.Collections.Generic;
using System.Linq;
using Negosud.Data;
using Negosud.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Negosud.Services
{
    public class SupplierOrderService
    {
        private readonly NegosudContext _context;

        public SupplierOrderService(NegosudContext context)
        {
            _context = context;
        }

        public IEnumerable<SupplierOrder> GetAllSupplierOrders()
        {
            return _context.SupplierOrders.Include(so => so.Supplier).ToList();
        }
        public void AddSupplierOrder(SupplierOrder supplierOrder)
        {
            _context.SupplierOrders.Add(supplierOrder);
            _context.SaveChanges();
        }

        public void UpdateSupplierOrder(SupplierOrder supplierOrder)
        {
            var existingOrder = _context.SupplierOrders
                .Include(so => so.SupplierOrderDetails)
                .FirstOrDefault(so => so.SupplierOrderID == supplierOrder.SupplierOrderID);

            if (existingOrder != null)
            {
                existingOrder.SupplierID = supplierOrder.SupplierID;
                existingOrder.OrderDate = supplierOrder.OrderDate;
                existingOrder.TotalAmount = supplierOrder.TotalAmount;

                foreach (var detail in supplierOrder.SupplierOrderDetails)
                {
                    var existingDetail = existingOrder.SupplierOrderDetails
                        .FirstOrDefault(d => d.ProductID == detail.ProductID);

                    if (existingDetail != null)
                    {
                        existingDetail.Quantity = detail.Quantity;
                    }
                    else
                    {
                        existingOrder.SupplierOrderDetails.Add(detail);
                    }
                }

                _context.SaveChanges();
            }
        }

        public void DeleteSupplierOrder(int supplierOrderId)
        {
            var order = _context.SupplierOrders
                .Include(so => so.SupplierOrderDetails)
                .FirstOrDefault(so => so.SupplierOrderID == supplierOrderId);

            if (order != null)
            {
                _context.SupplierOrders.Remove(order);
                _context.SaveChanges();
            }
        }

        public SupplierOrder GetSupplierOrderById(int supplierOrderId)
        {
            return _context.SupplierOrders
                .Include(so => so.Supplier)
                .Include(so => so.SupplierOrderDetails)
                    .ThenInclude(detail => detail.Product)
                .FirstOrDefault(so => so.SupplierOrderID == supplierOrderId);
        }
    }
}
