namespace E_CommerceFurnitureBackend.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }

        public int ProductId { get; set; }
        public int PhoneNummber { get; set; }
        public string CustomerCity {  get; set; }
        public DateTime OrderTime { get; set; }
        public int OrderStatus { get; set; }

    }
}
