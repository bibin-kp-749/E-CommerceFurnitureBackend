using E_CommerceFurnitureBackend.Models.DTO;

namespace E_CommerceFurnitureBackend.Services.UserServices
{
    public interface IUserServices
    {
       Task<Boolean> RegisterUser(UserDto userDto);
       Task<List<UserDto>> ViewAllUsers();
       Task<UserDto> ViewUserById(int Id);
       Task<Boolean> LoginUser(LoginDto user);
    }
}
