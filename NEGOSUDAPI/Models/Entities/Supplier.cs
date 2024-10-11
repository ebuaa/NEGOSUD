using Microsoft.AspNetCore.Identity;
using NEGOSUDAPI.Models.Entities;

namespace NEGOSUDAPI.Models.Entities
{
    public class Supplier
    {
        public int SupplierID { get; set; } // Primary Key
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        //NAvigation Property
        public ICollection<Product>? Products { get; set; }
    }
}