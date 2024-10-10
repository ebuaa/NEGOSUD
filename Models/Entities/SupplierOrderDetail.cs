using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negosud.Models.Entities
{
    public class SupplierOrderDetail
    {
        public int SupplierOrderDetailID { get; set; }
        public int SupplierOrderID { get; set; }
        public SupplierOrder SupplierOrder { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
