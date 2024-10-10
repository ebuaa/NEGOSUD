using Negosud.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negosud.Models.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; } 
        public decimal PricePerUnit { get; set; } 
        public int StockQuantity { get; set; }
        public int MinimumStock { get; set; }

  
        public int CategoryID { get; set; }
        public Category? Category { get; set; } 

    
        public int SupplierID { get; set; }
        public Supplier? Supplier { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<OrderDetail>? OrderDetails { get; set; }
        public virtual ICollection<SupplierOrderDetail> SupplierOrderDetails { get; set; }
    }
}

