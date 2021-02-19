using AutoMapper;
using DTOs = Accessors.DataTransferObjects;

namespace Managers.Mapping
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Contracts.Order, DTOs.Order>()
                .ReverseMap();
        }
    }
}
