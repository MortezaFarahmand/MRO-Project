using BasicDataManagement.Application;
using BasicDataManagement.Application.Contract.Entiti;
using BasicDataManagement.Domain.EntitiAgg;
using BasicDataManagement.Infrastructure.EFCore;
using BasicDataManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BasicDataManagement.Configuration
{
    public class BasicDataManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IEntitiApplication, EntitiApplication>();
            services.AddTransient<IEntitiRepository, EntitiRepository>();



            services.AddDbContext<BasicDataContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
