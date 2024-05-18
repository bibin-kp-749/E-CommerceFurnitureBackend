using AutoMapper;
using E_CommerceFurnitureBackend.DbCo;
using E_CommerceFurnitureBackend.Models;
using E_CommerceFurnitureBackend.Models.DTO;

namespace E_CommerceFurnitureBackend.Services.WishListServices
{
    public class WishListServices
    {
        private readonly IMapper _mapper;
        private readonly UserDbContext _userDbContext;
        public WishListServices(IMapper mapper,UserDbContext userDbContext)
        {
            this._mapper = mapper;
            this._userDbContext = userDbContext;
        }
        public async Task<Boolean> AddWishList(WishListDto wishlist)
        {
            try
            {
                var data = await _userDbContext.WishLists.AddAsync(_mapper.Map<WishList>(wishlist));
                await _userDbContext.SaveChangesAsync();
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }
    }
}
