using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace NEGOSUD.Models.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public string? Name { get; set; }
        public decimal PricePerUnit { get; set; }
        public int StockQuantity { get; set; }
        public int MinimumStock { get; set; }

        // Foreign Key
        public int CategoryID { get; set; } // References Category
        [ValidateNever]
        public Category? Category { get; set; } // Navigation property to Category

        // Foreign Keys
        public int SupplierID { get; set; }
        [ValidateNever]
        public Supplier? Supplier { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public string ImageUrl { get; set; }
        public string? Description { get; set; }


        // Navigation properties
        [ValidateNever]
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
