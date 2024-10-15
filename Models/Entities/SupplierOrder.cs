using System;
using System.Collections.Generic;

namespace Negosud.Models.Entities
{
    public class SupplierOrder
    {
        public int SupplierOrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int SupplierID { get; set; }
        public Supplier? Supplier { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<SupplierOrderDetail> SupplierOrderDetails { get; set; } = new List<SupplierOrderDetail>();
    }
}
