using System.ComponentModel.DataAnnotations;

namespace E_CommerceFurnitureBackend.Models.DTO
{
    public class ProductDto
    {
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
    }
}
