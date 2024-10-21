namespace NEGOSUD.Models.Entities
{
    public class OrderDetailViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
