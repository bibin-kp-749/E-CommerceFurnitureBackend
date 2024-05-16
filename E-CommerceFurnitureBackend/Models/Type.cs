using System.Collections;

namespace E_CommerceFurnitureBackend.Models
{
    public class Type
    {
        public int TypeId { get; set; }
        public string? TypeName { get; set; }
        public List<Product> Product { get; set; }
    }
}
