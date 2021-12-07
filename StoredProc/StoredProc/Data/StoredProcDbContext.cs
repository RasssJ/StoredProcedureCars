using Microsoft.EntityFrameworkCore;
using StoredProc.Models;
using StoredProcedure.Models;

namespace StoredProc.Data
{
    public class StoredProcDbContext : DbContext
    {
        public StoredProcDbContext(DbContextOptions<StoredProcDbContext> options)
            : base(options) { }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Car> Cars { get; set; }

    }
}
