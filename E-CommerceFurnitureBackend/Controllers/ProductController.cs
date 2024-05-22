﻿using E_CommerceFurnitureBackend.Models;
using E_CommerceFurnitureBackend.Models.DTO;
using E_CommerceFurnitureBackend.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("{ProductId}")]
        public async Task<IActionResult> GetProductById(int ProductId)
        {
            try
            {
                if (ProductId == 0 )
                    return BadRequest("Id can not contain zero or Null value");
                var product = await _productServices.ViewProductById(ProductId);
                if (product.ProductName == null)
                    return NotFound("Product Not Found"); 
                return Ok(product);                    
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An Unexpected error occurred{ex.Message}");
            }

        }
        [HttpGet("{category}")]
        public async Task<IActionResult> ViewProductByCategory(string category)
        {
            try
            {
                if(category == null||category== "^\\s+")
                    return BadRequest("category field is required");
                var product = await _productServices.ViewProductByCategory(category);
                if (product == null)
                    return NotFound("Product Not Found");       
                    return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An Unexpected error occurred{ex.Message}");
            }
        }
        [HttpPost("add-product")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct(ProductDto product)
        {
            //try
            //{
                if (product == null)
                    return BadRequest("Please fill all the fields");
                    var data=await _productServices.AddProduct(product);
                    if (!data)
                        return StatusCode(409,"Product Already existed");
                return Ok("Updated Successfully");
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, $"An Unexpected error occurred{ex.Message}");
            //}

        }
        [HttpPut("update-product")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int produtId, ProductDto product)
        {
            try
            {
                var response = await _productServices.UpdateProduct(produtId, product);
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            try
            {
                if ( Id == 0)
                    return BadRequest("Id can not contain zero or Null value");
                var response =await _productServices.DeleteProduct(Id);
                if (!response)
                    return BadRequest("Item not found");
                return Ok("Product Removed successfully");
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
                if(response.Count==0)
                    return BadRequest("Products Not found");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500,$"An Unexpected error occurred{ex.Message}");
            }
        }
        [HttpPost("{category}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNewCategory(CategoryDto category)
        {
            try
            {
                if (category.CategoryName.Length < 1)
                    return BadRequest("please fill the field");
                var response = await _productServices.AddNewCategory(category);
                if (response)
                    return StatusCode(201, "Successfully created product");
                return StatusCode(409, "Category Already existed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An Unexpected error occurred{ex.Message}");
            }
        }
        [HttpGet("{searchItem}")]
        public async Task<IActionResult> SearchProduct(string searchItem)
        {
            try
            {
                if (string.IsNullOrEmpty(searchItem))
                    return BadRequest();
                var response=await _productServices.SearchProduct(searchItem);
                if (response.Count<1)
                    return NotFound("Item Not Found");
                return Ok(response);
            }catch (Exception ex)
            {
                return StatusCode(500,$"An unexpected error occured {ex.Message}");
            }
        }
        [HttpGet("paginated")]
        public async Task<IActionResult> GetProductByPaginated(int PageNumber, int PageSize)
        {
            try
            {
                return Ok(_productServices.GetProductByPaginated(PageNumber, PageSize).Result);
            }catch(Exception ex)
            {
                return StatusCode(500, $"An unexpected error occured {ex.Message}");
            }
        }

    }
}
