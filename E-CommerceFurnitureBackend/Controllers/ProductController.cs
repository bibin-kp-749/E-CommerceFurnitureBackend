using E_CommerceFurnitureBackend.Models;
using E_CommerceFurnitureBackend.Models.DTO;
using E_CommerceFurnitureBackend.Services.ProductServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceFurnitureBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController:ControllerBase
    {
        private readonly IProductServices _productServices;
        public ProductController(IProductServices productServices)
        {
            this._productServices = productServices;
        }
        [HttpGet("/:ProductId")]
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
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An Unexpected error occurred{ex.Message}");
            }

        }
        [HttpGet("/:productcategory")]
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
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An Unexpected error occurred{ex.Message}");
            }
        }
        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct(ProductDto product)
        {
            try
            {
                if (product!= null)
                {
                    var data=await _productServices.CreateProduct(product);
                    if (data)
                        return Ok("Updated Successfully");
                    else
                        return StatusCode(409,"Product Already existed");
                }
                return BadRequest("Please fill all the fields");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An Unexpected error occurred{ex.Message}");
            }

        }
        [HttpPut("update-product")]
        public async Task<IActionResult> UpdateProduct(int Id, ProductDto product)
        {
            try
            {
                var response = await _productServices.UpdateProduct(Id, product);
                if (response)
                    return Ok("Successfully Updated");
                return NotFound("Item not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An Unexpected error occurred{ex.Message}");
            }
        }
        [HttpDelete("delete-product")]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            try
            {
                if (Id == null || Id == 0)
                    return BadRequest("Id can not contain zero or Null value");
                var response =await _productServices.DeleteProduct(Id);
                if (response)
                    return Ok("Product Removed successfully");
                else
                    return BadRequest("Product does not exist please Check your Id");
            }
            catch (Exception ex)
            {
                return StatusCode(500,$"An Unexpected error occurred{ex.Message}");
            }

        }
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> ViewAllProducts()
        {
            try
            {
                var response =await _productServices.ViewAllProducts();
                if(response==null)
                    return BadRequest("Products Not found");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500,$"An Unexpected error occurred{ex.Message}");
            }
        }
        [HttpPost("category")]
        public async Task<IActionResult> AddNewCategory(CategoryDto category)
        {
            try
            {
                if (category.CategoryName.Length < 1)
                    return BadRequest("please fill the field");
                var response = await _productServices.AddNewCategory(category);
                if (response)
                    return StatusCode(201, "Successfully created product");
                return StatusCode(500, "Internal server error ");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An Unexpected error occurred{ex.Message}");
            }
        }
        [HttpGet(":searchString")]
        public async Task<IActionResult> SearchProduct(string searchItem)
        {
            //try
            //{
                if (string.IsNullOrEmpty(searchItem))
                    return BadRequest();
                var response=await _productServices.SearchProduct(searchItem);
                if (response == null)
                    return NotFound();
                return Ok(response);
            //}catch (Exception ex)
            //{
            //    return StatusCode(500,"jii");
            //}
        }
    }
}
