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
        public async Task<IActionResult> RegisterUser(UserDto userDto)
        {
            try
            {
            if (userDto==null)
                return BadRequest("Please Fill all The Fields");
                var response=await _userServices.RegisterUser(userDto);
                if (response)
                    return StatusCode(204,"Registered Successfully");
                else
                    return StatusCode(500,"Registration Failed Please Retry");
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"An error occured while accessing the database : {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An Unexpected error occurred{ex.Message}");
            }
        }
        [HttpGet("GetAllUser")]
        public async Task<IActionResult> ViewAllUsers()
        {
            try
            {
                var response = await _userServices.ViewAllUsers();
                if (response == null)
                    return NotFound("Users Not found");
                return Ok(response);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"An error occured while accessing the database : {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An Unexpected error occurred{ex.Message}");
            }
        }
        [HttpGet("GetUserById")]
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
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"An error occured while accessing the database : {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An Unexpected error occurred{ex.Message}");
            }
        }
        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser(LoginDto user)
        {
            try
            {
                if (user.Email == null || user.Password == null)
                    return BadRequest("Please fill all the fields");
                var response= await _userServices.LoginUser(user);
                if (response)
                    return StatusCode(204, "Login successfully");
                return StatusCode(404,"User not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An Unexpected error occurred{ex.Message}");
            }
        }

    }
}
