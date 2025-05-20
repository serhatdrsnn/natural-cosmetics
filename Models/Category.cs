using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NaturalCosmeticsECommerce.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        [StringLength(250)]
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}