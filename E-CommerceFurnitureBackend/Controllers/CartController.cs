//using E_CommerceFurnitureBackend.Models.DTO;
//using E_CommerceFurnitureBackend.Models;
//using E_CommerceFurnitureBackend.Services.CartServices;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore.ChangeTracking;

//namespace E_CommerceFurnitureBackend.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CartController : ControllerBase
//    {
//        private readonly ICartServices cartServices;
//        public CartController(ICartServices cartServices)
//        {
//            this.cartServices = cartServices;
//        }
//        [HttpPost("cart/:productId")]
//        public async Task<IActionResult> AddProductToCart(int UserId, CartItemsDto items)
//        {
//            return Ok(cartServices.AddProductToCart(UserId, items));
//        }
//    }
//}
