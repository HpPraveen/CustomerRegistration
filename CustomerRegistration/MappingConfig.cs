using AutoMapper;
using CustomerRegistration.Models;
using CustomerRegistration.Models.DTO;

namespace Saruwata.Service.AdvertisingApi
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
        }
    }
}
