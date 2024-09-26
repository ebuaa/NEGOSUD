namespace NEGOSUD.Models.Entities
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public ICollection<Product> Products { get; set; }
        // Ensure this navigation property is present
        public ICollection<SupplierOrder> SupplierOrders { get; set; } // For Supplier Orders
    }
}
