using E_CommerceFurnitureBackend.Models;
using E_CommerceFurnitureBackend.Services.ProductServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceFurnitureBackend.Controllers
{
    public class ProductController
    {
        private readonly IProductServices _productServices;
        public ProductController(IProductServices productServices)
        {
            this._productServices = productServices;
        }
        [HttpGet("hi")]
        public async Task<Product> GetProductById(int id)
        {
            var res = _productServices.ViewProductById(id);
        }
    }
}
