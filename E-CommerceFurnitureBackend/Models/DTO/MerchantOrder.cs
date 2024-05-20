namespace E_CommerceFurnitureBackend.Models.DTO
{
    public class MerchantOrder
    {
        public string OrderId { get; set; }    
        public string RazorPayKey { get; set;}
        public int Amount { get; set;}
        public string Currency { get; set;}
        public string Name { get; set;}
        public string Email { get; set;}
        public int PhoneNumber { get; set; }
        public string Address { get; set;}
        public string Description { get; set;}

    }
}
