using _01_MROQuery.Contracts.OrganizationGroup;
using _01_MROQuery.Contracts.Slides;
using _01_MROQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrganizationManagement.Application;
using OrganizationManagement.Application.Contracts.ApprovalAuthority;
using OrganizationManagement.Application.Contracts.Organization;
using OrganizationManagement.Application.Contracts.OrganizationAviationCode;
using OrganizationManagement.Application.Contracts.Country;
using OrganizationManagement.Application.Contracts.OrganizationPicture;
using OrganizationManagement.Application.Contracts.Slide;
using OrganizationManagement.Domain.ApprovalAutorityAgg;
using OrganizationManagement.Domain.OrganizationAgg;
using OrganizationManagement.Domain.OrganizationAviationCodeAgg;
using OrganizationManagement.Domain.OrganizationGroupAgg;
using OrganizationManagement.Domain.OrganizationPictureAgg;
using OrganizationManagement.Domain.SlideAgg;
using OrganizationManagement.Infrastructure.EFCore;
using OrganizationManagement.Infrastructure.EFCore.Repository;

namespace OrganizationManagement.Configuration
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
