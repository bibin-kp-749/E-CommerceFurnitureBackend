using AutoMapper;
using E_CommerceFurnitureBackend.DbCo;
using E_CommerceFurnitureBackend.Models;
using E_CommerceFurnitureBackend.Models.DTO;
using E_CommerceFurnitureBackend.Services.JwtServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Razorpay.Api;
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
        public async Task<bool> AddProductToCartItem(string token,int productId)
        {
            try
            {
                var response=await _jwtServices.GetUserIdFromToken(token);
                var userId = Convert.ToInt32(response);
                if(userId== null||userId==0) 
                    return false;
                var data =await _userDbContext.Cart.Include(c=>c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);
                if (data != null)
                {
                    var IsExist = data.CartItems.FirstOrDefault(c => c.CartId == data.CartId && c.ProductId == productId);
                    if (IsExist!=null)
                        return false;
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
                    await _userDbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal Server error occured {ex.Message}");
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
                    return new List<ProductDto>();
                var cartItems =await  _userDbContext.CartItems.Where(c=>c.CartId==data.CartId).Include(p=>p.Product).ToListAsync();
                var products= cartItems.Select(p=>p.Product).ToList();
            return _mapper.Map<List<ProductDto>>(products);
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal server error{ex.Message}");
            }
        }
        public async Task<bool> DeleteItemsInCart(string token,int productId)
        {
            try
            {
                var response = await _jwtServices.GetUserIdFromToken(token);
                var userId = Convert.ToInt32(response);
            var data = await _userDbContext.Cart.FirstOrDefaultAsync(c => c.UserId == userId);
            var cartItems =  _userDbContext.CartItems.Where(c => c.ProductId==productId&&c.CartId==data.CartId);
                if (cartItems==null)
                    return false;
                await cartItems.ExecuteDeleteAsync();
            await _userDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal server error{ex.Message}");
            }
        }
        public async Task<bool> UpdateItemsInCart(string token,int productId,int value )
        {
            try
            {
                var response = await _jwtServices.GetUserIdFromToken(token);
                var userId = Convert.ToInt32(response);
                if (userId<1)
                    throw new Exception("Something went wrong with userId");
                var IsExict = await _userDbContext.Cart.FirstOrDefaultAsync(c => c.UserId == userId);
                var data = await _userDbContext.CartItems.FirstOrDefaultAsync(c => c.CartId == IsExict.CartId && c.ProductId == productId);
                if (data == null)
                    return false;
                data.Quantity =  data.Quantity + value;
                await _userDbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception($"Internal server error {ex.Message}");
            }
        }
    }
}
