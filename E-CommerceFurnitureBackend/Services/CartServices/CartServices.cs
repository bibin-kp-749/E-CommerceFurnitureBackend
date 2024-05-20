using AutoMapper;
using E_CommerceFurnitureBackend.DbCo;
using E_CommerceFurnitureBackend.Models;
using E_CommerceFurnitureBackend.Models.DTO;
using E_CommerceFurnitureBackend.Services.JwtServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace E_CommerceFurnitureBackend.Services.CartServices
{
    public class CartServices:ICartServices
    {
        private readonly UserDbContext _userDbContext;
        private readonly IMapper _mapper;
        private readonly IJwtServices _jwtServices;
        public CartServices(IMapper mapper,UserDbContext userDbContext,IJwtServices jwtServices)
        {
            this._mapper = mapper;
            this._userDbContext = userDbContext;
            this._jwtServices = jwtServices;
        }
        public async Task<Boolean> AddProductToCartItem(string token,int productId)
        {
            try
            {
                var response=await _jwtServices.GetUserIdFromToken(token);
                var userId = Convert.ToInt32(response);
                var data = _userDbContext.Cart.FirstOrDefault(c => c.UserId == userId);
                if (data != null)
                {
                    await _userDbContext.CartItems.AddAsync(new CartItems { CartId = data.CartId, ProductId = productId });
                    await _userDbContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    await _userDbContext.Cart.AddAsync(new Cart { UserId = userId });
                    await _userDbContext.SaveChangesAsync();
                    var cart = await _userDbContext.Cart.FirstOrDefaultAsync(s => s.UserId == userId);
                    await _userDbContext.CartItems.AddAsync(new CartItems { CartId = cart.CartId, ProductId = productId });
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<List<ProductDto>> GetItemsInCart(string token)
        {
            try
            {
                var response = await _jwtServices.GetUserIdFromToken(token);
                var userId = Convert.ToInt32(response);
                var data = await _userDbContext.Cart.FirstOrDefaultAsync(c => c.UserId == userId);
                if (data == null)
                    return null;
                var cartItems =await  _userDbContext.CartItems.Where(c=>c.CartId==data.CartId).Include(p=>p.Product).ToListAsync();
                var products= cartItems.Select(p=>p.Product).ToList();
            return _mapper.Map<List<ProductDto>>(products);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
