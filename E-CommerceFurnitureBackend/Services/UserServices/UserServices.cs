using AutoMapper;
using BCrypt.Net;
using E_CommerceFurnitureBackend.DbCo;
using E_CommerceFurnitureBackend.Models;
using E_CommerceFurnitureBackend.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_CommerceFurnitureBackend.Services.UserServices
{
    public class UserServices: IUserServices
    {
        private readonly UserDbContext _userDbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public UserServices(UserDbContext userDbContext,IMapper mapper,IConfiguration configuration)
        {
            this._userDbContext = userDbContext;
            this._mapper = mapper;
            this._configuration = configuration;
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
        public async Task<string> LoginUser(LoginDto user)
        {
            try
            {
                var data = await _userDbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                var passsword = BCrypt.Net.BCrypt.EnhancedVerify(user.Password, data.Password, HashType.SHA256);
                if (passsword && data!=null)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_configuration["JwtConfig:Key"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject=new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.NameIdentifier,data.UserId.ToString()),
                            new Claim(ClaimTypes.Name,data.UserName),
                            new Claim(ClaimTypes.Role,data.Role),
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(10),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token=tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString=tokenHandler.WriteToken(token);
                    return tokenString;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
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
