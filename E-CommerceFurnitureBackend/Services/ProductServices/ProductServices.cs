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
    }
}
