﻿using AutoMapper;
using E_CommerceFurnitureBackend.DbCo;
using E_CommerceFurnitureBackend.Models;
using E_CommerceFurnitureBackend.Models.DTO;
using System.IdentityModel.Tokens.Jwt;

namespace E_CommerceFurnitureBackend.Services.WishListServices
{
    public class WishListServices: IWishListServices
    {
        private readonly IMapper _mapper;
        private readonly UserDbContext _userDbContext;
        public WishListServices(IMapper mapper,UserDbContext userDbContext)
        {
            this._mapper = mapper;
            this._userDbContext = userDbContext;
        }
        public async Task<Boolean> AddWishList(string token, int ProdctId)
        {
            try
            {
                var handler =new JwtSecurityTokenHandler();
                var SecurityToken=handler.ReadJwtToken(token);
                var userId = SecurityToken.Claims.FirstOrDefault(s => s.Type == "nameid")?.Value;
                //var id=Convert.ToInt32(userId);
                var data = await _userDbContext.WishLists.AddAsync(new WishList { UserId=Convert.ToInt32(userId),ProductId=ProdctId });
                await _userDbContext.SaveChangesAsync();
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }
    }
}
