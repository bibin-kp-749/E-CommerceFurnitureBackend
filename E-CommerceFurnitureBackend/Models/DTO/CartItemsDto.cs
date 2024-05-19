using System.ComponentModel.DataAnnotations;

namespace E_CommerceFurnitureBackend.Models.DTO
{
    public class CartItemsDto
    {
        [Required]
        public int CartId { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
