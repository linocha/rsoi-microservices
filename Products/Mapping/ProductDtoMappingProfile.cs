using AutoMapper;
using Products.Domain.Models;
using Products.Resources;

namespace Products.Mapping
{
    public class ProductDtoMappingProfile : Profile
    {
        public ProductDtoMappingProfile()
        {
            CreateMap<Product, ProductResource>();
            CreateMap<SaveProductResource, Product>();
        }
    }
}