using E_CommerceFurnitureBackend.Models.DTO;

namespace E_CommerceFurnitureBackend.Services.CartServices
{
    public interface ICartServices
    {
        Task<Boolean> AddProductToCartItem(string token, int productId);
        Task<List<ProductDto>> GetItemsInCart(string token);
    }
}
