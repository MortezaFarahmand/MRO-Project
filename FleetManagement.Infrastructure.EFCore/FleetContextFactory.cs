using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FleetManagement.Infrastructure.EFCore
{
    public class FleetContextFactory : IDesignTimeDbContextFactory<FleetContext>
    {
        public FleetContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FleetContext>();
            optionsBuilder.UseSqlServer(
                "Server=.;Database=MRO_DB;Trusted_Connection=True;TrustServerCertificate=True");

            return new FleetContext(optionsBuilder.Options);
        }
    }
}