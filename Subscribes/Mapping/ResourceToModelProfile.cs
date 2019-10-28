using AutoMapper;
using Subscribes.Domain.Models;
using Subscribes.Resources;

namespace Subscribes.Mapping
{
    // post
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveSubscribeResource, Subscribe>();
        }
    }
}