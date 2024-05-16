using System.ComponentModel.DataAnnotations;

namespace E_CommerceFurnitureBackend.Models.DTO
{
    public class UserDto
    {
        [StringLength(50, MinimumLength = 4)]
        [Required]
        public string? UserName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [StringLength(16, MinimumLength = 8)]
        [Required]
        public string Password { get; set; }
    }
}
