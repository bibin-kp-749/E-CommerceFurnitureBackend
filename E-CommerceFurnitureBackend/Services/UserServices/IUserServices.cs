using E_CommerceFurnitureBackend.Models.DTO;

namespace E_CommerceFurnitureBackend.Services.UserServices
{
    public interface IUserServices
    {
       Task<bool> RegisterUser(UserDto userDto);
       Task<List<UserDto>> ViewAllUsers();
       Task<UserDto> ViewUserById(int userId);
       Task<String> LoginUser(LoginDto user);
       Task<bool> BlockUser(int id);
       Task<bool> UnBlockUser(int id);
    }
}
