namespace NEGOSUD.Models.Entities
{
    public class OrderViewModel
    {
        public decimal TotalAmount { get; set; }
        public List<OrderDetailViewModel> OrderDetails { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
