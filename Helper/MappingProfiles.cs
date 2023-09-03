using AutoMapper;
using smart_dental_webapi.Entity;
using smart_dental_webapi.Models.Customer;

namespace smart_dental_webapi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CustomerPostRequestModel, CustomerEntity>();
        }
    }
}
