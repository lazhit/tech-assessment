using AutoMapper;
using Contracts = Managers.Contracts;
using DTOs = Accessors.DataTransferObjects;

namespace Managers.Mapping
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Contracts.Customer, DTOs.Customer>()
                .ReverseMap();
        }
    }
}
