namespace E_CommerceFurnitureBackend.Models.DTO
{
    public class PaymentDto
    {
        public string Name {  get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public string Address { get; set; }
        public int Amount { get; set; }
    }
}
