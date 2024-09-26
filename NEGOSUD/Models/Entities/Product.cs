namespace NEGOSUD.Models.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public int SupplierID { get; set; }
        public Supplier Supplier { get; set; }

        public string ImageName { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } // For Orders
        public ICollection<SupplierOrderDetail> SupplierOrderDetails { get; set; } // For Supplier Orders
    }
}
