//using AutoMapper;
//using E_CommerceFurnitureBackend.DbCo;
//using E_CommerceFurnitureBackend.Models.DTO;
//using Microsoft.AspNetCore.Mvc;

//namespace E_CommerceFurnitureBackend.Services.CartServices
//{
//    public class CartServices
//    {
//        private readonly UserDbContext _userDbContext;
//        private readonly IMapper _mapper;
//        public CartServices(IMapper mapper,UserDbContext userDbContext)
//        {
//            this._mapper = mapper;
//            this._userDbContext = userDbContext;
//        }
//        public async Task<Boolean> AddProductToCart(int UserId, CartItemsDto items)
//        {
//            if (UserId < 1 || UserId == null)
//                return false;
//            var Isexist = _userDbContext.Users.FindAsync(UserId);
//        }
//    }
//}
