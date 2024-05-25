using E_CommerceFurnitureBackend.Models;
using E_CommerceFurnitureBackend.Models.DTO;

namespace E_CommerceFurnitureBackend.Services.ProductServices
{
    public interface IProductServices
    {
        Task<ViewProductDto> ViewProductById(int productId);
        Task<List<ViewProductDto>> ViewProductByCategory(string category);
        Task<bool> AddProduct(ProductDto product, IFormFile Image);
        Task<bool> DeleteProduct(int Id);
        Task<List<ViewProductDto>> ViewAllProducts();
        Task<bool> UpdateProduct(int Id, ProductDto product, IFormFile Image);
        Task<bool> AddNewCategory(string category);
        Task<List<ViewProductDto>> SearchProduct(string searchItem);
        Task<List<ViewProductDto>> GetProductByPaginated(int PageNumber, int PageSize);
        Task<bool> DeleteCategory(int categoryId);

    }
}
