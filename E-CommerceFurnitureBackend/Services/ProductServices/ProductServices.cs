using E_CommerceFurnitureBackend.DbCo;
using E_CommerceFurnitureBackend.Models;

namespace E_CommerceFurnitureBackend.Services.ProductServices
{
    public class ProductServices: IProductServices
    {
        private readonly UserDbContext _userDbContext;
        public ProductServices(UserDbContext userDbContext)
        {
            this._userDbContext = userDbContext;
        }
        public async Task<Product> ViewProductById(int productId)
        {
             var product = _userDbContext.Products.FirstOrDefault(p => p.ProductId == productId);
             return product;
        }
    }
}
