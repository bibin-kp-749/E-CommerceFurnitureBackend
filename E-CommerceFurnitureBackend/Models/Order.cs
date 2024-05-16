namespace E_CommerceFurnitureBackend.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail {  get; set; }
        public int CustomerPhoneNumber { get; set; }
        public string CustomerCity {  get; set; }
        public string CustomerHomeAddress {  get; set; }
        public DateTime OrderTime { get; set; }
        public int OrderStatus { get; set; }
        public User User { get; set; }
        public List<OrderItem> orderItems { get; set; }

    }
}
