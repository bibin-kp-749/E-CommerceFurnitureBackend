using AutoMapper;
using E_CommerceFurnitureBackend.Models;
using E_CommerceFurnitureBackend.Models.DTO;

namespace E_CommerceFurnitureBackend.Mapper
{
    public class Mappers:Profile
    {
        public Mappers()
        {
            CreateMap<User,UserDto>().ReverseMap();
            CreateMap<Product,ProductDto>().ReverseMap();
        }
    }
}
