using AutoMapper;
using Users.Domain.Models;
using Users.Resources;

namespace Users.Mapping
{
    public class UserDtoMappingProfile : Profile
    {
        public UserDtoMappingProfile()
        {
            CreateMap<User, UserResource>();
            CreateMap<UpdateUserResource, User>();
            CreateMap<SaveUserResource, User>();
        }
    }
}