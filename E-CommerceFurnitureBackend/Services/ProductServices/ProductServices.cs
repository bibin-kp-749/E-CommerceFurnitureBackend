using AutoMapper;
using E_CommerceFurnitureBackend.DbCo;
using E_CommerceFurnitureBackend.Models;
using E_CommerceFurnitureBackend.Models.DTO;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace E_CommerceFurnitureBackend.Services.ProductServices
{
    public class ProductServices: IProductServices
    {
        private readonly UserDbContext _userDbContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductServices(UserDbContext userDbContext,IMapper mapper,IWebHostEnvironment webHostEnvironment)
        {
            this._userDbContext = userDbContext;
            this._mapper = mapper;
            this._webHostEnvironment = webHostEnvironment;
        }
        public async Task<ProductDto> ViewProductById(int productId)
        {
            var data =await _userDbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (data == null)
                return new ProductDto();
            return _mapper.Map<ProductDto>(data);
        }
        public async Task<List<ProductDto>> ViewProductByCategory(string category)
        {
            var data = await _userDbContext.Products.Include(c => c.categories).Where(c => c.categories.CategoryName == category).ToListAsync();
            if (data == null)
                return new List<ProductDto>();
            return _mapper.Map<List<ProductDto>>(data);   
        }
        public async Task<bool> AddProduct(ProductDto product, IFormFile Image)
        {
            try
            {
                var IsExist = await _userDbContext.Products.AnyAsync(p => p.ProductName == product.ProductName);
                if (IsExist)
                    return false;
                if (Image != null)
                {
                    string? productImage = null;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Upload", "Product", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }

                    productImage = "/Uploads/Product/" + fileName;
                    var value = _mapper.Map<Product>(product);
                    value.Image = productImage;
                    await _userDbContext.Products.AddAsync(value);
                    await _userDbContext.SaveChangesAsync();
                    return true;
                };
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Internal Server Error");
            }
        }
        public async Task<bool> DeleteProduct(int Id)
        {
            var product=await _userDbContext.Products.FirstOrDefaultAsync(p=>p.ProductId == Id);
            if (product == null)
                return false;
            _userDbContext.Products.Remove(product);
             _userDbContext.SaveChanges();
            return true;
        }
        public async Task<List<ProductDto>> ViewAllProducts()
        {
            var response=await _userDbContext.Products.ToListAsync();
            if (response==null)
                return new List<ProductDto>();
            return _mapper.Map<List<ProductDto>>(response);
        }
        public async Task<bool> UpdateProduct(int productId, ProductDto product, IFormFile Image)
        {
            string? productImage = null;
            try
            {var response= await _userDbContext.Products.FirstOrDefaultAsync(p=>p.ProductId== productId);
            if (response == null)
                return false;
            response.ProductName = product.ProductName;
            response.ProductCaption = product.ProductCaption;
            response.Category = product.Category;
            response.OriginalPrice = product.OriginalPrice;
            response.OfferPrice = product.OfferPrice;
            response.Type=product.Type;
                if (Image != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Upload", "Product", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }

                    productImage = "/Uploads/Product/" + fileName;
                }
                response.Image=productImage;
            _userDbContext.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw new Exception($"Internal server error {ex.Message}");
            }
        }
        public async Task<bool> AddNewCategory(string category)
        {
            var categories = _userDbContext.Categories.FirstOrDefault(s => s.CategoryName == category);
            if (categories == null)
            {
                var response = await _userDbContext.Categories.AddAsync(new Category { CategoryName=category});
                await _userDbContext.SaveChangesAsync();
                return true;
            }else 
                return false;
        }
        public async Task<List<ProductDto>> SearchProduct(string searchItem)
        {
            var product= _userDbContext.Products.Where(s=>s.ProductName.Contains(searchItem));
            if (product == null)    
               return new List<ProductDto>();
            return _mapper.Map<List<ProductDto>>(product);      
        }
        public async Task<List<ProductDto>> GetProductByPaginated(int PageNumber=1,int PageSize=10)
        {
            var totalCount = _userDbContext.Products.Count();
            var totalPage =(int)Math.Ceiling((decimal)totalCount / PageSize);
            var productsPerPage = _userDbContext.Products
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize);
            return _mapper.Map<List<ProductDto>>(productsPerPage);
        }
    }
}
