using E_CommerceFurnitureBackend.Models.DTO;
using E_CommerceFurnitureBackend.Services.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace E_CommerceFurnitureBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            this._userServices = userServices;
        }
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> Registraion(UserDto userDto)
        {
            try
            {
            if (userDto==null)
                return BadRequest("Please Fill all The Fields");
                _userServices.RegisterUser(userDto);
               return Ok("Registered Successfully");
            }catch (DbUpdateException ex)
            {
                return BadRequest($"An error occured while accessing the database : {ex.Message}");
            }catch (Exception ex)
            {
                return BadRequest($"An unexpected error occured : {ex.Message}");
            }
        }
    }
}
