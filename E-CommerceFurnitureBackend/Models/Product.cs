using System.ComponentModel.DataAnnotations;

namespace E_CommerceFurnitureBackend.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string? ProductName { get; set; }
        public string? ProductCaption { get; set; }
        [Required]
        public string? Image { get; set; }
        [Required]
        public int OriginalPrice { get; set; }
        public int OfferPrice { get; set; }
        [Required]
        public int Category { get; set; }
        [Required]
        public int Type { get; set; }
        public virtual List<CartItems> CartItems { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }
        public virtual List<WishList> wishLists { get; set; }
        public virtual Types types { get; set; }
        public virtual Category categories { get; set;}
    }
}
