using E_CommerceFurnitureBackend.Models.DTO;
using E_CommerceFurnitureBackend.Models;
using E_CommerceFurnitureBackend.Services.CartServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Authorization;

namespace E_CommerceFurnitureBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartServices cartServices;
        public CartController(ICartServices cartServices)
        {
            this.cartServices = cartServices;
        }
        [HttpPost("cart/:productId")]
        [Authorize]
        public async Task<IActionResult> AddProductToCart(string token,int productId)
        {
            try
            {
                if (token.Length < 1 || productId == null)
                    return BadRequest("Please fill all the field");
                var response = await cartServices.AddProductToCartItem(token, productId);
                if (response)
                    return Ok();
                return StatusCode(409, "Internal server error kk");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("get-all")]
        [Authorize]
        public async Task<IActionResult> GetItemsInCart(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                    return BadRequest();
                var response = await cartServices.GetItemsInCart(token);
                if (response.Count< 1)
                    return NotFound();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpeced error is occured{ex.Message}");
            }
        }
    }
}
