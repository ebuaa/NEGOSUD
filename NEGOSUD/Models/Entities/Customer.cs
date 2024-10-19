using Microsoft.AspNetCore.Identity;

namespace NEGOSUD.Models.Entities
{
    public class Customer : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? Telephone { get; set; }

        //Navigation property
        public ICollection<Order>? Orders { get; set; }
    }
}
