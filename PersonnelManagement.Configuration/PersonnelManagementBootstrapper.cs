using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonnelManagement.Application;
using PersonnelManagement.Application.Contracts.PersonGroup;
using PersonnelManagement.Domain.PersonGroupAgg;
using PersonnelManagement.Infrastructure.EFCore.Repository;
using PersonnelnManagement.Infrastructure.EFCore;

namespace PersonManagement.Configuration
{
    public class PersonnelManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IPersonGroupApplication, PersonGroupApplication>();
            services.AddTransient<IPersonGroupRepository, PersonGroupRepository>();



            services.AddDbContext<PersonnelContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
