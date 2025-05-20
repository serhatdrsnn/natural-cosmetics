using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NaturalCosmeticsECommerce.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Ürün adı zorunludur")]
        [StringLength(100, ErrorMessage = "Ürün adı maksimum 100 karakter olabilir")]
        [Display(Name = "Ürün Adı")]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Açıklama")]
        [StringLength(500, ErrorMessage = "Açıklama maksimum 500 karakter olabilir")]
        public string Description { get; set; } = "Açıklama bulunmamaktadır";
        [Required(ErrorMessage = "Fiyat zorunludur")]
        [Column(TypeName = "decimal(10, 2)")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }
        [Display(Name = "Resim URL")]
        public string ImageUrl { get; set; } = "/img/default.png";
        [Required(ErrorMessage = "Kategori seçimi zorunludur")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [Display(Name = "Kategori")]
        public Category? Category { get; set; }
        [Required(ErrorMessage = "Stok miktarı zorunludur")]
        [Range(0, int.MaxValue, ErrorMessage = "Stok miktarı negatif olamaz")]
        [Display(Name = "Stok Miktarı")]
        public int StockQuantity { get; set; } = 0;
    }
}