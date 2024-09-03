namespace NEGOSUD.Models.Entities
{
    public class Customer
    {
        public int CustumerID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
