using E_CommerceFurnitureBackend.Models.DTO;
using E_CommerceFurnitureBackend.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace E_CommerceFurnitureBackend.Services.CartServices
{

    public interface ICartServices
    {
         Task AddProductToCart(int UserId, CartItemsDto items);
    }
}
