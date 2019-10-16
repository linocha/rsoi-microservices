using AutoMapper;
using Products.Domain.Models;
using Products.Resources;

namespace Products.Mapping
{
    // get
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Product, ProductResource>();
        }
    }
}