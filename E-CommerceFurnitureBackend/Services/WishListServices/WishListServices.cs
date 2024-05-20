using AutoMapper;
using E_CommerceFurnitureBackend.DbCo;
using E_CommerceFurnitureBackend.Models;
using E_CommerceFurnitureBackend.Models.DTO;
using E_CommerceFurnitureBackend.Services.JwtServices;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace E_CommerceFurnitureBackend.Services.WishListServices
{
    public class WishListServices: IWishListServices
    {
        private readonly IMapper _mapper;
        private readonly UserDbContext _userDbContext;
        private readonly IJwtServices _jwtServices;
        public WishListServices(IMapper mapper,UserDbContext userDbContext,IJwtServices jwtServices)
        {
            this._mapper = mapper;
            this._userDbContext = userDbContext;
            this._jwtServices = jwtServices;
        }
        public async Task<Boolean> AddWishList(string token, int ProdctId)
        {
            try
            {
                var response = await _jwtServices.GetUserIdFromToken(token);
                var userId = Convert.ToInt32(response);
                var data = await _userDbContext.WishLists.AddAsync(new WishList { UserId=Convert.ToInt32(userId),ProductId=ProdctId });
                await _userDbContext.SaveChangesAsync();
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<List<ProductDto>> GetItemsInWishList(string token)
        {
            try
            {
                var response = await _jwtServices.GetUserIdFromToken(token);
                var userId = Convert.ToInt32(response); var data = await _userDbContext.WishLists.Where(p => p.UserId == Convert.ToInt32(userId)).Include(p=>p.product).ToListAsync();
                var product= data.Select(s=>s.product).ToList();
                return _mapper.Map<List<ProductDto>>(product) ;
            }catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Boolean> DeleteTheWishListItem(int productId,string token)
        {
            try
            {
                var response = await _jwtServices.GetUserIdFromToken(token);
                var userId = Convert.ToInt32(response);
                var data = _userDbContext.WishLists.Where(p => p.UserId == Convert.ToInt32(userId) && p.ProductId == productId).ExecuteDeleteAsync();
                await _userDbContext.SaveChangesAsync();
                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }
    }
}
