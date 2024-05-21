using E_CommerceFurnitureBackend.Models.DTO;
using E_CommerceFurnitureBackend.Services.WishListServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost(":id")]
        public async Task<IActionResult> AddWishList(string token, int ProdctId)
        {
            try
            {
                if (ProdctId == 0 || ProdctId == null || token.Length < 1)
                    return BadRequest("Please fill all the fields");
               var response=await _services.AddWishList(token,ProdctId);
                if(response)
                    return Ok(response);
                return StatusCode(409,"Item already Present");
            }catch (Exception ex)
            {
                return StatusCode(500,$"An unexpected error is occure{ex.Message}");
            }
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetItemsInWishList(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                    return BadRequest();
                var response = await _services.GetItemsInWishList(token);
                if (response.Count < 1)
                    return NotFound();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error is occure{ex.Message}");
            }
        }
        [HttpDelete(":itemId")]
        public async Task<IActionResult> DeleteTheWishListItem(int productId, string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token)||productId==0||productId==null)
                    return BadRequest();
                var response=await _services.DeleteTheWishListItem(productId, token);
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
