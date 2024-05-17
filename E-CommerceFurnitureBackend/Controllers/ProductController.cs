using E_CommerceFurnitureBackend.Models;
using E_CommerceFurnitureBackend.Models.DTO;
using E_CommerceFurnitureBackend.Services.ProductServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceFurnitureBackend.Controllers
{
    public class ProductController:ControllerBase
    {
        private readonly IProductServices _productServices;
        public ProductController(IProductServices productServices)
        {
            this._productServices = productServices;
        }
        [HttpGet("Product/id")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                if (id != 0 || id!=null) {
                    var product = await _productServices.ViewProductById(id);
                    if (product != null)
                    {
                        return Ok(product);
                    }
                    return NotFound("Product Not Found");
                }else 
                    return BadRequest("Id can not contain zero or Null value"); 
            }catch (DbUpdateException ex)
            {
                return BadRequest($"An error occured while accessing the database : {ex.Message}");
            }catch(Exception ex)
            {
                return BadRequest($"An Unexpected error occurred{ex.Message}");
            }

        }
        [HttpGet("Product/Category")]
        public async Task<IActionResult> ViewProductByCategory(string category)
        {
            try
            {
                if(category != null||category!= "^\\s+")
                {
                    var product = await _productServices.ViewProductByCategory(category);
                    if (product != null)
                        return Ok(product);
                    return NotFound("Product Not Found");
                }
                else
                    return BadRequest("category field is required");
            }catch(DbUpdateException ex)
            {
                return BadRequest($"An error occured while accessing the database : {ex.Message}");
            }catch(Exception ex)
            {
                return BadRequest($"An Unexpected error occurred{ex.Message}");
            }
        }
    }
}
