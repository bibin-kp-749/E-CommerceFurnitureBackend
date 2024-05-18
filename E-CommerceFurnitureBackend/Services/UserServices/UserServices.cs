using AutoMapper;
using BCrypt.Net;
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
                var HashPasswor=BCrypt.Net.BCrypt.EnhancedHashPassword(userDto.Password,HashType.SHA256);
                userDto.Password=HashPasswor;
                await _userDbContext.Users.AddAsync(_mapper.Map<User>(userDto));
                await _userDbContext.SaveChangesAsync();
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
        public async Task<Boolean> LoginUser(LoginDto user)
        {
            var data=await _userDbContext.Users.FirstOrDefaultAsync(u=>u.Email==user.Email);
            var passsword = BCrypt.Net.BCrypt.EnhancedVerify(user.Password, data.Password, HashType.SHA256);
            if(passsword)
                return true;
            return false;
        }
        public async Task<Boolean> BlockUser(int id)
        {
            var data= await _userDbContext.Users.FirstOrDefaultAsync(u=>u.UserId==id);
            if(data == null)
                return false;
            data.Isstatus = false;
            _userDbContext.SaveChanges();
            return true;
        }
        public async Task<Boolean> UnBlockUser(int id)
        {
            var data=await _userDbContext.Users.FirstOrDefaultAsync(u=>u.UserId==id);
            if(data == null)
                return false;
            data.Isstatus = true;
            _userDbContext.SaveChanges();
            return true;
        }
    }
}
