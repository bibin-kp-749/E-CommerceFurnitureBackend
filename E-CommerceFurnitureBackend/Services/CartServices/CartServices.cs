using AutoMapper;
using E_CommerceFurnitureBackend.DbCo;
using E_CommerceFurnitureBackend.Models;
using E_CommerceFurnitureBackend.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace E_CommerceFurnitureBackend.Services.CartServices
{
    public class CartServices: ICartServices
    {
        private readonly UserDbContext _userDbContext;
        private readonly IMapper _mapper;
        public CartServices(UserDbContext userDbContext,IMapper mapper)
        {
            this._userDbContext = userDbContext;
            this._mapper = mapper;
        }
        public async Task AddProductToCart(int UserId,CartItemsDto items)
        {
            try
            {
                var cartid = await _userDbContext.Cart.FirstOrDefaultAsync(c => c.UserId == UserId);
                if (cartid.CartId != null)
                {
                   await _userDbContext.CartItems.AddAsync(new CartItems { CartId=cartid.CartId,ProductId=items.ProductId});
                   await _userDbContext.SaveChangesAsync();
                }else
                {
                    await _userDbContext.Cart.AddAsync(new Cart { UserId = UserId });
                    var cartidcreated = await _userDbContext.Cart.FirstOrDefaultAsync(c => c.UserId == UserId);
                    _userDbContext.CartItems.Add(new CartItems { CartId=cartidcreated.CartId, ProductId=items.ProductId });
                    await _userDbContext.SaveChangesAsync();
                }
            }catch (Exception ex)
            { 
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }
    }
}
