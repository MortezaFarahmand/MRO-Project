using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonnelManagement.Application;
using PersonnelManagement.Application.Contracts.Person;
using PersonnelManagement.Application.Contracts.PersonGroup;
using PersonnelManagement.Application.Contracts.PersonPicture;
using PersonnelManagement.Domain.PersonAgg;
using PersonnelManagement.Domain.PersonGroupAgg;
using PersonnelManagement.Domain.PersonPictureAgg;
using PersonnelManagement.Infrastructure.EFCore;
using PersonnelManagement.Infrastructure.EFCore.Repository;

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

            services.AddTransient<IPersonPictureApplication, PersonPictureApplication>();
            services.AddTransient<IPersonPictureRepository, PersonPictureRepository>();



            services.AddDbContext<PersonnelContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
