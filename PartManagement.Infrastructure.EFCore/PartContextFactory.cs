using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PartManagement.Infrastructure.EFCore
{
    public class PartContextFactory : IDesignTimeDbContextFactory<PartContext>
    {
        public PartContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PartContext>();

            // Connection string را مستقیم بگذارید (فقط برای Migration)
            optionsBuilder.UseSqlServer(
                "Server=.;Database=MRO_DB;Trusted_Connection=True;TrustServerCertificate=True");

            return new PartContext(optionsBuilder.Options);
        }
    }
}