using AutoMapper;
using E_CommerceFurnitureBackend.DbCo;
using E_CommerceFurnitureBackend.Models;
using E_CommerceFurnitureBackend.Models.DTO;

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
        public async Task RegisterUser(UserDto userDto)
        {
            _userDbContext.Users.Add(_mapper.Map<User>(userDto));
            _userDbContext.SaveChanges();
        }
    }
}
