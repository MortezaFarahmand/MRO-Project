using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BasicDataManagement.Infrastructure.EFCore
{
    public class BasicDataContextFactory : IDesignTimeDbContextFactory<BasicDataContext>
    {
        public BasicDataContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "ServiceHost");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<BasicDataContext>();

            // نام کانکشن استرینگ را از appsettings.json بردار
            var connectionString = configuration.GetConnectionString("MROdb");

            optionsBuilder.UseSqlServer(connectionString);

            return new BasicDataContext(optionsBuilder.Options);
        }
    }
}