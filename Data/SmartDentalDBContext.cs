using Microsoft.EntityFrameworkCore;
using smart_dental_webapi.Entity;

namespace smart_dental_webapi.Data
{
    public class SmartDentalDBContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public SmartDentalDBContext(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
    }
}
