using Microsoft.AspNetCore.Identity;

namespace NEGOSUD.Models.Entities
{
    public class Customer : IdentityUser
    {
        //public string Name { get; set; }
        //public string Address { get; set; }

        //Navigation property
        public ICollection<Order>? Orders { get; set; }
    }
}
