using System.ComponentModel.DataAnnotations;

namespace E_CommerceFurnitureBackend.Models
{
    public class WishList
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ProductId { get; set; }
        public virtual User user { get; set; }
        public virtual Product product { get; set; }
    }
}
