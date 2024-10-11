using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace NEGOSUDAPI.Models.Entities
{
    public class Customer : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? Telephone { get; set; }

        //Navigation property
        [JsonIgnore]
        public ICollection<Order>? Orders { get; set; }
    }
}