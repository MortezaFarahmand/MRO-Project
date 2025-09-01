using _01_MROQuery.Contracts.OrganizationGroup;
using _01_MROQuery.Contracts.Slides;
using _01_MROQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BasicDatanManagement.Application;
using BasicDatanManagement.Application.Contracts.ApprovalAuthority;
using BasicDatanManagement.Application.Contracts.Organization;
using BasicDatanManagement.Application.Contracts.OrganizationAviationCode;
using BasicDatanManagement.Application.Contracts.OrganizationGroup;
using BasicDatanManagement.Application.Contracts.OrganizationPicture;
using BasicDatanManagement.Application.Contracts.Slide;
using BasicDatanManagement.Domain.ApprovalAutorityAgg;
using BasicDatanManagement.Domain.OrganizationAgg;
using BasicDatanManagement.Domain.OrganizationAviationCodeAgg;
using BasicDatanManagement.Domain.OrganizationGroupAgg;
using BasicDatanManagement.Domain.OrganizationPictureAgg;
using BasicDatanManagement.Domain.SlideAgg;
using BasicDatanManagement.Infrastructure.EFCore;
using BasicDatanManagement.Infrastructure.EFCore.Repository;

namespace BasicDatanManagement.Configuration
{
    public class OrganizationManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IOrganizationGroupApplication, OrganizationGroupApplication>();
            services.AddTransient<IOrganizationGroupRepository, OrganizationGroupRepository>();

            services.AddTransient<IOrganizationApplication, OrganizationApplication>();
            services.AddTransient<IOrganizationRepository, OrganizationRepository>();

            services.AddTransient<IOrganizationPictureApplication, OrganizationPictureApplication>();
            services.AddTransient<IOrganizationPictureRepository, OrganizationPictureRepository>();

            services.AddTransient<ISlideApplication, SlideApplication>();
            services.AddTransient<ISlideRepository, SlideRepository>();

            //services.AddTransient<IPersonApplication, CountryApplication>();
            //services.AddTransient<IPersonRepository, CountryRepository>();

            services.AddTransient<IOrganizationAviationCodeApplication, OrganizationAviationCodeApplication>();
            services.AddTransient<IOrganizationAviationCodeRepository, OrganizationAviationCodeRepository>();

            services.AddTransient<IApprovalAuthorityApplication, ApprovalAuthorityApplication>();
            services.AddTransient<IApprovalAuthorityRepository, ApprovalAuthorityRepository>();


            services.AddTransient<ISlideQuery, SlideQuery>();
            services.AddTransient<IOrganizatonGroupQuery, OrganizationGroupQuery>();



            services.AddDbContext<OrganizationContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
