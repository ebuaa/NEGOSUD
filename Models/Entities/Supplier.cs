using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Negosud.Models.Entities
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Adresse e-mail invalide.")]
        public string Email { get; set; } 
        public string ContactInfo { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}


