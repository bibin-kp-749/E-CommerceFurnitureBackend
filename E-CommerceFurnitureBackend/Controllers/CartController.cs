﻿using E_CommerceFurnitureBackend.Models.DTO;
using E_CommerceFurnitureBackend.Models;
using E_CommerceFurnitureBackend.Services.CartServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
        public async Task<IActionResult> AddProductToCart(string token,int productId)
        {
            try
            {
                if (token.Length < 1 || productId == null)
                    return BadRequest();
                var response = await cartServices.AddProductToCartItem(token, productId);
                if (response)
                    return Ok();
                return StatusCode(500, "Internal server error");
            }catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
