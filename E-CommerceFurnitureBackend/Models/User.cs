namespace E_CommerceFurnitureBackend.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public bool Isstatus { get; set; }
        public Cart Cart { get; set; }
        public List<Order> Order { get; set; }
    }
}
