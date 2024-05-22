﻿using E_CommerceFurnitureBackend.Models;
using E_CommerceFurnitureBackend.Models.DTO;

namespace E_CommerceFurnitureBackend.Services.ProductServices
{
    public interface IProductServices
    {
        Task<ProductDto> ViewProductById(int productId);
        Task<List<ProductDto>> ViewProductByCategory(string category);
        Task<bool> AddProduct(ProductDto product);
        Task<bool> DeleteProduct(int Id);
        Task<List<ProductDto>> ViewAllProducts();
        Task<bool> UpdateProduct(int Id, ProductDto product);
        Task<bool> AddNewCategory(CategoryDto category);
        Task<List<ProductDto>> SearchProduct(string searchItem);
        Task<List<ProductDto>> GetProductByPaginated(int PageNumber, int PageSize);

    }
}
