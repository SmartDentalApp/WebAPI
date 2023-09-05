using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using smart_dental_webapi.Entity;
using smart_dental_webapi.Models.Customer;
using smart_dental_webapi.Repositories.Customer;
using smart_dental_webapi.Services.Customer;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomerService(IMapper mapper, ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public bool CreateCustomer(CustomerPostRequestModel customerCreate)
    {
        var customerMap = _mapper.Map<CustomerEntity>(customerCreate);
        return _customerRepository.CreateCustomer(customerMap);
    }

    public bool IsCustomerCreateValid(CustomerPostRequestModel customerCreate, ModelStateDictionary modelState)
    {
        if (customerCreate == null)
        {
            modelState.AddModelError("", "Dado de criação nulo.");
            return false;
        }

        if (!modelState.IsValid)
        {
            modelState.AddModelError("", "Dado inválido.");
            return false;
        }

        var existingCustomer = _customerRepository.GetCustomers()
            .FirstOrDefault(c => c.Email.Trim().ToUpper() == customerCreate.Email.TrimEnd().ToUpper());

        if (existingCustomer != null)
        {
            modelState.AddModelError("", "Cliente já cadastrado.");
            return false;
        }


        return true;
    }
}
