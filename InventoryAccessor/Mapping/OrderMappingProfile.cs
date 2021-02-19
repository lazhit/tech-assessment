using AutoMapper;
using DTOs = Accessors.DataTransferObjects;

namespace Accessors.Mapping
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Entities.Order, DTOs.Order>()
                .ReverseMap();
        }
    }
}
