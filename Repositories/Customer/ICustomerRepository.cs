using smart_dental_webapi.Entity;

namespace PokemonReviewApp.Interfaces
{
    public interface ICustomerRepository
    {
        bool CreateCustomer(CustomerEntity customer);
        ICollection<CustomerEntity> GetCustomers();
    }
}
