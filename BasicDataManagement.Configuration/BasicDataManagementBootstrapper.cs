using BasicDataManagement.Application;
using BasicDataManagement.Application.Contract.Entiti;
using BasicDataManagement.Application.Contracts.Country;
using BasicDataManagement.Domain.CountryAgg;
using BasicDataManagement.Domain.EntitiAgg;
using BasicDataManagement.Infrastructure.EFCore;
using BasicDataManagement.Infrastructure.EFCore.Repository;
using BasicDatanManagement.Application;
using BasicDatanManagement.Infrastructure.EFCore.Repository;
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

            services.AddTransient<ICountryApplication, CountryApplication>();
            services.AddTransient<ICountryRepository, CountryRepository>();

            services.AddDbContext<BasicDataContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
