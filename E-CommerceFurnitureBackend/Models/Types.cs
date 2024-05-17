using System.Collections;

namespace E_CommerceFurnitureBackend.Models
{
    public class Types
    {
        public int Id { get; set; }
        public string? TypeName { get; set; }
        public virtual List<Product> Product { get; set; }
    }
}
