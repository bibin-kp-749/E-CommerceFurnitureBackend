using E_CommerceFurnitureBackend.Models.DTO;
using E_CommerceFurnitureBackend.Services.WishListServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceFurnitureBackend.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListServices _services;
        public WishListController(IWishListServices wishListServices)
        {
            this._services = wishListServices;
        }
        [HttpPost("wishlist/:id")]
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
    }
}
