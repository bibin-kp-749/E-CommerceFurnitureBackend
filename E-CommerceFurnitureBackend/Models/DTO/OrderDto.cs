using System.ComponentModel.DataAnnotations;

namespace E_CommerceFurnitureBackend.Models.DTO
{
    public class OrderDto
    {
            public int OrderId { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public string CustomerName { get; set; }
            public string CustomerEmail { get; set; }
            public int CustomerPhoneNumber { get; set; }
            public string CustomerCity { get; set; }
            public string CustomerHomeAddress { get; set; }
            public DateTime OrderTime { get; set; }
            public int OrderStatus { get; set; }
    }
}
