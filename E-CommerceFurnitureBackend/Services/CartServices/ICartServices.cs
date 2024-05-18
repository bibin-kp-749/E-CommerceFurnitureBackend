namespace E_CommerceFurnitureBackend.Services.CartServices
{
    public interface ICartServices
    {
        Task<Boolean> AddProductToCartItem(string token, int productId);
    }
}
