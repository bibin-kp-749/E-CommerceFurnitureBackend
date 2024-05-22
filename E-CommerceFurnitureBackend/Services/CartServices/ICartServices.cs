using E_CommerceFurnitureBackend.Models.DTO;

namespace E_CommerceFurnitureBackend.Services.CartServices
{
    public interface ICartServices
    {
        Task<bool> AddProductToCartItem(string token, int productId);
        Task<List<ProductDto>> GetItemsInCart(string token);
        Task<bool> DeleteItemsInCart(string token, int productId);
        Task<bool> UpdateItemsInCart(string token, int productId, int value);
    }
}
