namespace E_CommerceFurnitureBackend.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<CartItems> CartItems { get; set; }
    }
}
