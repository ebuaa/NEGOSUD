namespace NEGOSUD.Models.Entities
{
    public class SupplierOrder
    {
        public int SupplierOrderID { get; set; }
        public int SupplierID { get; set; }
        public Supplier Supplier { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal TotalAmount { get; set; }

        public string OrderStatus { get; set; }

        public ICollection<SupplierOrderDetail> SupplierOrderDetails { get; set; }
    }
}
