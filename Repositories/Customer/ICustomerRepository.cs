using smart_dental_webapi.Entity;

namespace smart_dental_webapi.Repositories.Customer
{
    public interface ICustomerRepository
    {
        bool CreateCustomer(CustomerEntity customer);
        ICollection<CustomerEntity> GetCustomers();
        CustomerEntity GetCustomer(int id);
    }
}
