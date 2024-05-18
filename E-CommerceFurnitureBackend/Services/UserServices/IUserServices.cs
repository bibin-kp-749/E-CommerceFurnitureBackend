using E_CommerceFurnitureBackend.Models.DTO;

namespace E_CommerceFurnitureBackend.Services.UserServices
{
    public interface IUserServices
    {
       Task<Boolean> RegisterUser(UserDto userDto);
       Task<List<UserDto>> ViewAllUsers();
       Task<UserDto> ViewUserById(int Id);
       Task<String> LoginUser(LoginDto user);
       Task<Boolean> BlockUser(int id);
       Task<Boolean> UnBlockUser(int id);
    }
}
