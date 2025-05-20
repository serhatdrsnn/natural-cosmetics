using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NaturalCosmeticsECommerce.Models
{
   public enum OrderStatus
{
    [Display(Name = "Beklemede")]
    Pending,
    [Display(Name = "Tamamlandı")]
    Completed,
    [Display(Name = "İptal Edildi")]
    Cancelled
}
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        [Column(TypeName = "varchar(255)")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [Required]
        public string ShippingAddress { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        // Items koleksiyonunu constructor'da başlat
        public Order()
        {
            Items = new List<CartItem>();
        }
        public ICollection<CartItem> Items { get; set; }
    }
}
