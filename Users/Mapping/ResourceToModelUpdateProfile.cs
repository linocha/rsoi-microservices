using AutoMapper;
using Users.Domain.Models;
using Users.Resources;

namespace Users.Mapping
{
    public class ResourceToModelUpdate : Profile
    {
        public ResourceToModelUpdate()
        {
            CreateMap<UpdateUserResource, User>();
        }
    }
}