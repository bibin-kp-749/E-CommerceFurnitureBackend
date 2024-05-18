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
            var data = await _userDbContext.Products.Include(c => c.categories).Where(c => c.categories.CategoryName == category).ToListAsync();
            if (data != null)
                  return _mapper.Map<List<ProductDto>>(data);
            return null;
        }
        public async Task<bool> CreateProduct(ProductDto product)
        {
            try
            {
                var IsExist =await _userDbContext.Products.AnyAsync(p => p.ProductName == product.ProductName);
                if (IsExist)
                    return false;
                if (product.Image.Length==0||product.Image!=null)
                    return false;
                var value = _mapper.Map<Product>(product);
                await _userDbContext.Products.AddAsync(value);
                await _userDbContext.SaveChangesAsync();
                return true;           
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
        public async Task<List<ProductDto>> ViewAllProducts()
        {
            var response=await _userDbContext.Products.ToListAsync();
            if (response==null)
                return null;
            return _mapper.Map<List<ProductDto>>(response);
        }
        public async Task<Boolean> UpdateProduct(int Id, ProductDto product)
        {
            var response= await _userDbContext.Products.FirstOrDefaultAsync(p=>p.ProductId==Id);
            if (response == null)
                return false;
            response.ProductName = product.ProductName;
            response.ProductCaption = product.ProductCaption;
            response.Category = product.Category;
            response.Image = product.Image;
            response.OriginalPrice = product.OriginalPrice;
            response.OfferPrice = product.OfferPrice;
            response.Type=product.Type;
            _userDbContext.SaveChanges();
            return true;
        }
        public async Task<Boolean> AddNewCategory(CategoryDto category)
        {
            if (category == null)
                return false;
            var data = _mapper.Map<Category>(category);
            var response = await _userDbContext.Categories.AddAsync(data);
            await _userDbContext.SaveChangesAsync();
            return true;
        }
    }
}
