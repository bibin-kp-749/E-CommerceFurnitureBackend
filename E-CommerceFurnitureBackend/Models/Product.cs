namespace E_CommerceFurnitureBackend.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductCaption { get; set;}
        public int ProductPrice { get; set; }
        public string? ProductUrl { get; set;}
        public int ProductCategory { get; set; }
        public int ProductType { get; set;}
    }
}
