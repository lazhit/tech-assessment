using AutoMapper;
using DTOs = Accessors.DataTransferObjects;

namespace Accessors.Mapping
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Entities.Customer, DTOs.Customer>()
                .ReverseMap();
        }
    }
}
