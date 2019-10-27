using AutoMapper;
using Users.Domain.Models;
using Users.Resources;

namespace Users.Mapping
{
    // get
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<User, UserResource>();
        }
    }
}