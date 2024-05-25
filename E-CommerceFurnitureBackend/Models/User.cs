using System.ComponentModel.DataAnnotations;

namespace E_CommerceFurnitureBackend.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [StringLength(50, MinimumLength = 4)]
        [Required]
        public string? UserName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [StringLength(100, MinimumLength = 8)]
        [Required]
        public string Password { get; set; }
        public bool Isstatus { get; set; } = true;
        public string Role { get; set; } = "User";
        public virtual Cart Cart { get; set; }
        public virtual List<Order> Order { get; set; }
        public virtual List<WishList> WhishLists { get; set; }

    }
}
