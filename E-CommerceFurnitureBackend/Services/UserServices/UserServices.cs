using AutoMapper;
using E_CommerceFurnitureBackend.DbCo;
using E_CommerceFurnitureBackend.Models;
using E_CommerceFurnitureBackend.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace E_CommerceFurnitureBackend.Services.UserServices
{
    public class UserServices: IUserServices
    {
        private readonly UserDbContext _userDbContext;
        private readonly IMapper _mapper;
        public UserServices(UserDbContext userDbContext,IMapper mapper)
        {
            this._userDbContext = userDbContext;
            this._mapper = mapper;
        }
        public async Task<Boolean> RegisterUser(UserDto userDto)
        {
            try
            {
                if (userDto.Email.Length < 1 || userDto.Password.Length < 1)
                    return false;
                _userDbContext.Users.Add(_mapper.Map<User>(userDto));
                _userDbContext.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return false;
            }
        }
        public async Task<List<UserDto>> ViewAllUsers()
        {
                var users =await _userDbContext.Users.ToListAsync();
                if (users == null)
                    return null;
                return  _mapper.Map<List<UserDto>>(users);
        }
        public async Task<UserDto> ViewUserById(int Id)
        {
            var user=await _userDbContext.Users.SingleOrDefaultAsync(u=>u.UserId == Id);
            if (user == null) 
                return null;
            return _mapper.Map<UserDto>(user);
        }
    }
}
