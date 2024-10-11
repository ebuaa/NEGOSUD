using NEGOSUDAPI.Models.Entities;

namespace NEGOSUDAPI.Models.Entities
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }

        // Foreign Keys
        public int OrderID { get; set; }
        public Order? Order { get; set; }

        public int ProductID { get; set; }
        public Product? Product { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
