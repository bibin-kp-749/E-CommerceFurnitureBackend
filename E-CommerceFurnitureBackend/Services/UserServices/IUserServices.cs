using E_CommerceFurnitureBackend.Models.DTO;

namespace E_CommerceFurnitureBackend.Services.UserServices
{
    public interface IUserServices
    {
        Task RegisterUser(UserDto userDto);
    }
}
