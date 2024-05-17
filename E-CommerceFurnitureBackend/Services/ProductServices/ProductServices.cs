using AutoMapper;
using E_CommerceFurnitureBackend.DbCo;
using E_CommerceFurnitureBackend.Models;
using E_CommerceFurnitureBackend.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceFurnitureBackend.Services.ProductServices
{
    public class ProductServices: IProductServices
    {
        private readonly UserDbContext _userDbContext;
        private readonly IMapper _mapper;
        public ProductServices(UserDbContext userDbContext,IMapper mapper)
        {
            this._userDbContext = userDbContext;
            this._mapper = mapper;
        }
        public async Task<ProductDto> ViewProductById(int productId)
        {
            var data =await _userDbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            if(data != null)
            {
                var product=_mapper.Map<ProductDto>(data);
                return product;
            }
            return null;
        }
        public async Task<List<ProductDto>> ViewProductByCategory(string category)
        {
            List<ProductDto> products = new List<ProductDto>();
            var data = await _userDbContext.Products.Include(c => c.categories).Where(c => c.categories.CategoryName == category).ToListAsync();
            if (data != null)
            {
                foreach (var i in data)
                {
                    var product = _mapper.Map<ProductDto>(i);
                    products.Add(product);
                }
                return products;
            }
            return null;
        }
        public async Task<bool> CreateProduct(ProductDto product)
        {
            try
            {
                var IsExist =await _userDbContext.Products.AnyAsync(p => p.ProductName == product.ProductName);
                if (IsExist)
                    return false;
                if (product.Image.Length>0&&product.Image!=null)
                {
                    var value = _mapper.Map<Product>(product);
                    await _userDbContext.Products.AddAsync(value);
                    await _userDbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }catch(Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return false;
            }
        }
        public async Task<Boolean> DeleteProduct(int Id)
        {
            var product=await _userDbContext.Products.FirstOrDefaultAsync(p=>p.ProductId == Id);
            if (product == null)
                return false;
            _userDbContext.Products.Remove(product);
            await _userDbContext.SaveChangesAsync();
            return true;
        }
    }
}
