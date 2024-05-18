using System.ComponentModel.DataAnnotations;

namespace E_CommerceFurnitureBackend.Models.DTO
{
    public class WishListDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
