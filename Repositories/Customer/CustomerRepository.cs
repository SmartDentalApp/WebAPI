using smart_dental_webapi.Data;
using smart_dental_webapi.Entity;

namespace smart_dental_webapi.Repositories.Customer
{
    public class CustomerRepository : ICustomerRepository
    {
        private SmartDentalDBContext _context;

        public CustomerRepository(SmartDentalDBContext context)
        {
            _context = context;
        }

        public ICollection<CustomerEntity> GetCustomers()
        {
            return _context.Customers.OrderBy(c => c.Id).ToList();
        }

        public CustomerEntity GetCustomer(int id)
        {
            return _context.Customers.Where(c => c.Id == id).FirstOrDefault();
        }

        public bool CreateCustomer(CustomerEntity customer)
        {
            _context.Add(customer);

            return Save();
        }

        private bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
