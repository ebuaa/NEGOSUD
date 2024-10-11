using NEGOSUDAPI.Models.Entities;

namespace NEGOSUDAPI.Models.Entities
{
    public class Order //Domain Model => Communicates with the database only
    {
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }
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