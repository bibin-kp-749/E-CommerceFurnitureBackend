using E_CommerceFurnitureBackend.Models;
using E_CommerceFurnitureBackend.Models.DTO;

namespace E_CommerceFurnitureBackend.Services.ProductServices
{
    public interface IProductServices
    {
        Task<ProductDto> ViewProductById(int productId);
        Task<List<ProductDto>> ViewProductByCategory(string category);
        Task<Boolean> CreateProduct(ProductDto product);
        Task<Boolean> DeleteProduct(int Id);
        Task<List<ProductDto>> ViewAllProducts();
        Task<Boolean> UpdateProduct(int Id, ProductDto product);
        Task<Boolean> AddNewCategory(CategoryDto category);
        Task<List<ProductDto>> SearchProduct(string searchItem);
        Task<List<ProductDto>> GetProductByPaginated(int PageNumber, int PageSize);

    }
}
