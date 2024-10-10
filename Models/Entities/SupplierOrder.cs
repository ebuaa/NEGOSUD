using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negosud.Models.Entities
{
    public class SupplierOrder
    {
        public int SupplierOrderID { get; set; }
        public int SupplierID { get; set; }
        public Supplier Supplier { get; set; }
        public DateTime OrderDate { get; set; }

        public ICollection<SupplierOrderDetail> SupplierOrderDetails { get; set; }
    }
}
