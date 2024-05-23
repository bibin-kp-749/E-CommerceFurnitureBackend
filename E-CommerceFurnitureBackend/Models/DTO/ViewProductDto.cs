using System.ComponentModel.DataAnnotations;

namespace E_CommerceFurnitureBackend.Models.DTO
{
    public class ViewProductDto
    {
        public string? ProductName { get; set; }
        public string? ProductCaption { get; set; }
        public string Image { get; set; }
        public int OriginalPrice { get; set; }
        public int OfferPrice { get; set; }
        public int Category { get; set; }
        public int Type { get; set; }
    }
}
