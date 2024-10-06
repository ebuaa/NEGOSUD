namespace NEGOSUD.Models.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public string? Name { get; set; }
        public decimal PricePerUnit { get; set; }
        public int StockQuantity { get; set; }
        public int MinimumStock { get; set; }

        // Foreign Key
        public int CategoryID { get; set; } // References Category
        public Category? Category { get; set; } // Navigation property to Category

        // Foreign Keys
        public int SupplierID { get; set; }
        public Supplier? Supplier { get; set; }

        // Navigation properties
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
