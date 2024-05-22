using System.ComponentModel.DataAnnotations;

namespace E_CommerceFurnitureBackend.Models
{
    public class CartItems
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; } = 1;
        public virtual Cart Cart { get; set; }
        public virtual Product Product { get; set; }
    }
}
