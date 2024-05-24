using E_CommerceFurnitureBackend.Models.DTO;
using E_CommerceFurnitureBackend.Models;
using E_CommerceFurnitureBackend.Services.CartServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Authorization;
using E_CommerceFurnitureBackend.DbCo;
using E_CommerceFurnitureBackend.Services.JwtServices;

namespace E_CommerceFurnitureBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartServices cartServices;
        private readonly IJwtServices jwtServices;
        public CartController(ICartServices cartServices,IJwtServices jwtServices)
        {
            this.cartServices = cartServices;
            this.jwtServices = jwtServices;
        }
        [HttpPost("{productId}")]
        [Authorize]
        public async Task<IActionResult> AddProductToCart(int productId)
        {
            try
            {
                if (productId == 0 || productId == null)
                    return BadRequest("Product Id canot contain null or zero");
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];
                if (jwtToken.Length < 1)
                    return BadRequest("Token is not valid");
                var response = await cartServices.AddProductToCartItem(jwtToken, productId);
                if (response)
                    return Ok("Added successsfully");
                return StatusCode(409, "Item already Exist in the cart");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An Unexpected Error occured guyzz{ex.Message}");
            }
        }
        [HttpGet("get-all")]
        [Authorize]
        public async Task<IActionResult> GetItemsInCart()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];
                if (string.IsNullOrEmpty(jwtToken))
                    return BadRequest("Token is not valid");
                var response = await cartServices.GetItemsInCart(jwtToken);
                if (response.Count==0)
                    return NotFound();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpeced error is occured{ex.Message}");
            }
        }
        [HttpDelete("remove-product")]
        [Authorize]
        public async Task<IActionResult> DeleteItemsInCart(int productId)
        {
            try
            {
                if (productId == 0 || productId == null)
                    return BadRequest("Product Id can not contain null or zero");
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];
                if (string.IsNullOrEmpty(jwtToken))
                    return BadRequest("Token is not valid");
                var response=await cartServices.DeleteItemsInCart(jwtToken, productId);
                if(response)
                    return Ok("Removed Successfully");
                return NotFound("Product is not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500,$"An unexpected error is occured {ex.Message}");
            }
        }
        [HttpPut("Update-Count")]
        [Authorize]
        public async Task<IActionResult> UpdateItemsInCart( int productId, int value)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];
                if (string.IsNullOrEmpty(jwtToken) || productId < 1)
                    return BadRequest();
                var response = await cartServices.UpdateItemsInCart(jwtToken, productId,value);
                if (response)
                    return Ok();
                return NotFound("Product Not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error is occured {ex.Message}");
            }
        }
    }
}
