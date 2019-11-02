using Analysis.XUnit.Parallel.API.Model;
using Microsoft.EntityFrameworkCore;

namespace Analysis.XUnit.Parallel.API
{
    public partial class CustomerDbContext : DbContext
    {
        public CustomerDbContext()
        {
        }

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; } = null!;
    }
}
