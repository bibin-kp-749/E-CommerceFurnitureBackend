using System.ComponentModel.DataAnnotations;

namespace E_CommerceFurnitureBackend.Models.DTO
{
    public class LoginDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [StringLength(16, MinimumLength = 8)]
        [Required]
        public string Password { get; set; }
    }
}
