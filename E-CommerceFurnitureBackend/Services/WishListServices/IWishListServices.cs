using E_CommerceFurnitureBackend.Models.DTO;

namespace E_CommerceFurnitureBackend.Services.WishListServices
{
    public interface IWishListServices
    {
        Task<Boolean> AddWishList(string token ,int ProdctId);
        Task<List<ProductDto>> GetItemsInWishList(string token);
        Task<Boolean> DeleteTheWishListItem(int productId, string token);
    }
}
