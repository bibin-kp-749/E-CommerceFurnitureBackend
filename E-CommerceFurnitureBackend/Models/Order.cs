using System.ComponentModel.DataAnnotations;

namespace E_CommerceFurnitureBackend.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        [Required]
        [StringLength(30,MinimumLength =3)]
        public string CustomerName { get; set; }
        [Required]
        [EmailAddress]
        public string CustomerEmail {  get; set; }
        [Required]
        [Phone]
        public int CustomerPhoneNumber { get; set; }
        [Required]
        public string CustomerCity {  get; set; }
        [Required]
        public string CustomerHomeAddress {  get; set; }
        public DateTime OrderTime { get; set; }
        public int OrderStatus { get; set; }
        public User User { get; set; }
        public List<OrderItem> orderItems { get; set; }

    }
}
