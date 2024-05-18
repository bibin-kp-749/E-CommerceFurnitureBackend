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
        public async Task<IActionResult> SignUp(UserDto userDto)
        {
            try
            {
            if (userDto==null)
                return BadRequest("Please Fill all The Fields");
                var response=await _userServices.RegisterUser(userDto);
                if (response)
                    return Ok("Registered Successfully");
                else
                    return StatusCode(500,"Registration Failed Please Retry");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An Unexpected error occurred{ex.Message}");
            }
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> ViewAllUsers()
        {
            try
            {
                var response = await _userServices.ViewAllUsers();
                if (response == null)
                    return NotFound("Users Not found");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An Unexpected error occurred{ex.Message}");
            }
        }
        [HttpGet(":id")]
        public async Task<IActionResult> ViewUserById(int Id)
        {
            try
            {
                if (Id == 0 || Id == null)
                    return BadRequest("Id can not contain zero or Null value");
                var response= await _userServices.ViewUserById(Id);
                if (response == null)
                    return NotFound("User Not Found");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An Unexpected error occurred{ex.Message}");
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto user)
        {
            try
            {
                if (user.Email == null || user.Password == null)
                    return BadRequest("Please fill all the fields");
                var response= await _userServices.LoginUser(user);
                if (response)
                    return Ok("Login successfully");
                return StatusCode(404,"User not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An Unexpected error occurred{ex.Message}");
            }
        }
        [HttpPost("api/admin/login")]
        public async Task<IActionResult> AdminLogin(LoginDto user)
        {
            try
            {
                if (user.Email == null || user.Password == null)
                    return BadRequest("Please fill all the fields");
                var response = await _userServices.LoginUser(user);
                if (response)
                    return Ok("Login successfully");
                return StatusCode(404, "User not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An Unexpected error occurred{ex.Message}");
            }
        }
        [HttpPut("block/:id")]
        public async Task<IActionResult> BlockUser(int id)
        {
            try
            {
                if (id == 0 || id == null)
                    return BadRequest("Id canot contain null or zero");
                var response = await _userServices.BlockUser(id);
                if (!response)
                    return NotFound("User not found");
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An Unexpected error occurred{ex.Message}");
            }
        }
        [HttpPut("unblock/:id")]
        public async Task<IActionResult> UnBlockUser(int id)
        {
            try
            {
                if (id == 0 || id == null)
                    return BadRequest("Id canot contain null or zero");
                var response = await _userServices.UnBlockUser(id);
                if (!response)
                    return NotFound("User not found");
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An Unexpected error occurred{ex.Message}");
            }
        }

    }
}
