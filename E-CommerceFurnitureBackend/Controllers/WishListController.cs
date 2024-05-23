using E_CommerceFurnitureBackend.Models.DTO;
using E_CommerceFurnitureBackend.Services.WishListServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace E_CommerceFurnitureBackend.Controllers
{
    [Route("api/product/wishlist")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListServices _services;
        public WishListController(IWishListServices wishListServices)
        {
            this._services = wishListServices;
        }
        [HttpPost("AddWishList")]
        [Authorize]
        public async Task<IActionResult> AddWishList( int ProdctId)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];
                if (ProdctId == 0 || jwtToken.Length < 1)
                    return BadRequest("Please fill all the fields");
               var response=await _services.AddWishList(jwtToken, ProdctId);
                if(response)
                    return Ok(response);
                return StatusCode(409,"Item already Present");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error is occure{ex.Message}");
            }
        }
        [HttpGet("get-all")]
        [Authorize]
        public async Task<IActionResult> GetItemsInWishList()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];
                if (string.IsNullOrEmpty(jwtToken))
                    return BadRequest();
                var response = await _services.GetItemsInWishList(jwtToken);
                if (response.Count < 1)
                    return NotFound();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error is occure{ex.Message}");
            }
        }
        [HttpDelete("{itemId}")]
        [Authorize]
        public async Task<IActionResult> DeleteTheWishListItem(int itemId)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];
                if (string.IsNullOrEmpty(jwtToken) || itemId == 0)
                    return BadRequest();
                var response=await _services.DeleteTheWishListItem(itemId, jwtToken);
                if(response)
                return Ok("success");
                return NotFound("Item not found");
            }catch(Exception ex)
            {
                return StatusCode(500,$"An unexpected error occured {ex.Message}");
            }
        }
    }
}
