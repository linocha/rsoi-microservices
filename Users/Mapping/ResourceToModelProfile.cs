using AutoMapper;
using Users.Domain.Models;
using Users.Resources;

namespace Users.Mapping
{
    // post
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveUserResource, User>();
        }
    }
}