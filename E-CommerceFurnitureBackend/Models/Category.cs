namespace E_CommerceFurnitureBackend.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public virtual List<Product> Product { get; set; }
    }
}
