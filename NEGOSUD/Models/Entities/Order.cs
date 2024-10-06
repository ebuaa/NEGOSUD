namespace NEGOSUD.Models.Entities
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        // Foreign Key
        public string? CustomerID { get; set; }
        public Customer? Customer { get; set; }

        // Navigation property
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
