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
                if (ProdctId == 0 || ProdctId == null || token.Length < 30)
                    return BadRequest();
               var response=await _services.AddWishList(token,ProdctId);
                if(response)
                    return Ok(response);
                return StatusCode(500, "Internal Server Error");
            }catch (Exception ex)
            {
                return StatusCode(500,"Something went wrong");
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
                if (response!=null)
                    return Ok(response);
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong");
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
                return Ok();
                return NotFound();
            }catch(Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
