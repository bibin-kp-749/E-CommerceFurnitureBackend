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
        public async Task<bool> AddWishList(string token, int ProdctId)
        {
            try
            {
                var response = await _jwtServices.GetUserIdFromToken(token);
                var userId = Convert.ToInt32(response);
                var IsExist=await _userDbContext.WishLists.AnyAsync(x => x.UserId == userId && x.ProductId==ProdctId);
                if(IsExist)
                    return false;
                var data = await _userDbContext.WishLists.AddAsync(new WishList { UserId=Convert.ToInt32(userId),ProductId=ProdctId });
                await _userDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal Server error {ex.Message}");
            }
        }
        public async Task<List<ProductDto>> GetItemsInWishList(string token)
        {
            try
            {
                var response = await _jwtServices.GetUserIdFromToken(token);
                var userId = Convert.ToInt32(response); 
                var data = await _userDbContext.WishLists.Where(p => p.UserId == userId).Include(p=>p.product).ToListAsync();
                if (data.Count == 0)
                    return new List<ProductDto>();
                var product= data.Select(s=>s.product).ToList();
                return _mapper.Map<List<ProductDto>>(product) ;
            }catch (Exception ex)
            {
                return new List<ProductDto>();
            }
        }
        public async Task<bool> DeleteTheWishListItem(int productId,string token)
        {
            try
            {
                var response = await _jwtServices.GetUserIdFromToken(token);
                var userId = Convert.ToInt32(response);
                var data =await _userDbContext.WishLists.FirstOrDefaultAsync(p => p.UserId == userId && p.ProductId == productId);
                if(data == null)
                    return false;
                 _userDbContext.WishLists.Remove(data);
                await _userDbContext.SaveChangesAsync();
                return true;
            } catch (Exception ex)
            {
                throw new Exception($"Internal server error {ex.Message}");
            }
        }
    }
}
