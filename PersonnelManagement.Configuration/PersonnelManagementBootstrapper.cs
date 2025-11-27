using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonnelManagement.Application;
using PersonnelManagement.Application.Contracts.PersonGroup;
using PersonnelManagement.Domain.PersonGroupAgg;
using PersonnelManagement.Infrastructure.EFCore.Repository;
using PersonnelManagement.Infrastructure.EFCore;
using PersonnelManagement.Application.Contracts.Person;
using PersonnelManagement.Domain.PersonAgg;

namespace PersonManagement.Configuration
{
    public class PersonnelManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IPersonGroupApplication, PersonGroupApplication>();
            services.AddTransient<IPersonGroupRepository, PersonGroupRepository>();

            services.AddTransient<IPersonApplication, PersonApplication>();
            services.AddTransient<IPersonRepository, PersonRepository>();



            services.AddDbContext<PersonnelContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
