using E_CommerceFurnitureBackend.Models;

namespace E_CommerceFurnitureBackend.Services.ProductServices
{
    public interface IProductServices
    {
        Task<Product> ViewProductById(int productId);
    }
}
