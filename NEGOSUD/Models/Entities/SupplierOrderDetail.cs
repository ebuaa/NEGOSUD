namespace NEGOSUD.Models.Entities
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
