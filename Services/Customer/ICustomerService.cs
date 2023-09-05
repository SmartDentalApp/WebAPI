using Microsoft.AspNetCore.Mvc.ModelBinding;
using smart_dental_webapi.Entity;
using smart_dental_webapi.Models.Customer;

namespace smart_dental_webapi.Services.Customer
{
    public interface ICustomerService
    {
        bool CreateCustomer(CustomerPostRequestModel customerCreate);
        bool IsCustomerCreateValid(CustomerPostRequestModel customerCreate, ModelStateDictionary modelState);
    }
}
