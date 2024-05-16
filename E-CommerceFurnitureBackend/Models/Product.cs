namespace E_CommerceFurnitureBackend.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductCaption { get; set;}
        public string? Image { get; set;}
        public int OriginalPrice { get; set; }
        public int OfferPrice { get; set; }
        public int Category { get; set; }
        public int Type { get; set;}
        public List<CartItems> CartItems { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public Type types { get; set; }
        public Category categories { get; set;}
    }
}
