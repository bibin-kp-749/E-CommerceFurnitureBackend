using AutoMapper;
using E_CommerceFurnitureBackend.DbCo;
using E_CommerceFurnitureBackend.Models;
using E_CommerceFurnitureBackend.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace E_CommerceFurnitureBackend.Services.CartServices
{
    public class CartServices:ICartServices
    {
        private readonly UserDbContext _userDbContext;
        private readonly IMapper _mapper;
        public CartServices(IMapper mapper,UserDbContext userDbContext)
        {
            this._mapper = mapper;
            this._userDbContext = userDbContext;
        }
        public async Task<Boolean> AddProductToCartItem(string token,int productId)
        {
            try
            {
                //var handler = new JwtSecurityTokenHandler();
                //var SecurityToken=handler.ReadJwtToken(token);
                //var claim = SecurityToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
                //var userId = Convert.ToInt32(claim);
                var userId=
                var data = _userDbContext.Cart.FirstOrDefault(c => c.UserId == userId);
                if (data != null)
                {
                    _userDbContext.CartItems.Add(new CartItems { CartId = data.CartId, ProductId = productId });
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
                var handler = new JwtSecurityTokenHandler();
                var SecurityToken = handler.ReadJwtToken(token);
                var claim = SecurityToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
                var userId = Convert.ToInt32(claim);
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
