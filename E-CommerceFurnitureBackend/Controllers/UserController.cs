using E_CommerceFurnitureBackend.Models.DTO;
using E_CommerceFurnitureBackend.Services.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            if (userDto == null)
                return BadRequest("Please Fill all The Fields");
            _userServices.RegisterUser(userDto);
            return Ok();
        }
    }
}
