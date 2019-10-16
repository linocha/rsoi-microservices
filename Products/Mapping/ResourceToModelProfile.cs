using AutoMapper;
using Products.Domain.Models;
using Products.Resources;

namespace Products.Mapping
{
    // post
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveProductResource, Product>();
        }
    }
}