using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using NEGOSUDAPI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace NEGOSUDAPI.Models.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        public string? Name { get; set; }
        public decimal PricePerUnit { get; set; }
        public int StockQuantity { get; set; }
        public int MinimumStock { get; set; }
        public string? Description { get; set; }


        // Foreign Key
        public int CategoryID { get; set; } // References Category
        [ValidateNever]
        [JsonIgnore] // Ignore lors de la sérialisation pour éviter les cycles
        public Category? Category { get; set; } // Navigation property to Category

        // Foreign Keys
        public int SupplierID { get; set; }
        [ValidateNever]
        [JsonIgnore]
        public Supplier? Supplier { get; set; }

        [NotMapped]
        [JsonIgnore]
        public IFormFile? ImageFile { get; set; }
        public string ImageUrl { get; set; }


        // Navigation properties
        [ValidateNever]
        [JsonIgnore]
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}