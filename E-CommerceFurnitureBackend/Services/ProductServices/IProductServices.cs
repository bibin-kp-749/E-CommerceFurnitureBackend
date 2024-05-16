using E_CommerceFurnitureBackend.Models;
using E_CommerceFurnitureBackend.Models.DTO;

namespace E_CommerceFurnitureBackend.Services.ProductServices
{
    public interface IProductServices
    {
        Task<ProductDto> ViewProductById(int productId);
    }
}
