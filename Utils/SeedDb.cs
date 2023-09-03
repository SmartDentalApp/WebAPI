using smart_dental_webapi.Data;
using smart_dental_webapi.Entity;

namespace smart_dental_webapi.Utils
{
    public class SeedDb
    {
        private readonly SmartDentalDBContext _dbContext;
        public SeedDb(SmartDentalDBContext context)
        {
            _dbContext = context;
        }
        public void SeedDataContext()
        {
            if (!_dbContext.Customers.Any())
            {
                var customers = new List<CustomerEntity>()
                {
                    new CustomerEntity()
                    {
                        Address = "Rua teste, n 918",
                        BirthDate = DateTime.Now,
                        CPF = "07389896655",
                        Email = "igor.duartematos@gmail.com",
                        Id = 1,
                        Name = "Igor Duarte",
                        RG = "19269639",
                        TreatmentStartDate = DateTime.Now,
                    },
                    new CustomerEntity()
                    {
                        Address = "Rua cleto rasta, n 12312",
                        BirthDate = DateTime.Now,
                        CPF = "07389812322",
                        Email = "claudio.matos@hotmail.com",
                        Id = 2,
                        Name = "Claudio",
                        RG = "12322321",
                        TreatmentStartDate = DateTime.Now,
                    }
                };
                _dbContext.Customers.AddRange(customers);
                _dbContext.SaveChanges();
            }
        }
        public static void SeedData(IHost app)
        {
            var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

            using (var scope = scopedFactory.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<SeedDb>();
                service.SeedDataContext();
            }
        }
    }
}
