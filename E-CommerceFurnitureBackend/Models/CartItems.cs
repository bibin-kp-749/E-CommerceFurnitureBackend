namespace E_CommerceFurnitureBackend.Models
{
    public class CartItems
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}
