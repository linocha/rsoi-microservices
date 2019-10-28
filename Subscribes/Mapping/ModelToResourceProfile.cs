using AutoMapper;
using Subscribes.Domain.Models;
using Subscribes.Resources;

namespace Subscribes.Mapping
{
    // get
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Subscribe, SubscribeResource>();
        }
    }
}