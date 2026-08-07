using _01_MROQuery.Contracts.OrganizationGroup;
using _01_MROQuery.Contracts.Slides;
using _01_MROQuery.Query;
using BasicDataManagement.Application;
using BasicDataManagement.Application.Contracts.City;
using BasicDataManagement.Application.Contracts.Country;
using BasicDataManagement.Application.Contracts.Entiti;
using BasicDataManagement.Application.Contracts.PictureCategory;
using BasicDataManagement.Application.Contracts.Province;
using BasicDataManagement.Application.Contracts.Slide;
using BasicDataManagement.Domain.CityAgg;
using BasicDataManagement.Domain.CountryAgg;
using BasicDataManagement.Domain.EntitiAgg;
using BasicDataManagement.Domain.PictureCategoryAgg;
using BasicDataManagement.Domain.ProvinceAgg;
using BasicDataManagement.Domain.SlideAgg;
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

            services.AddTransient<ICountryApplication, CountryApplication>();
            services.AddTransient<ICountryRepository, CountryRepository>();

            services.AddTransient<IProvinceApplication, ProvinceApplication>();
            services.AddTransient<IProvinceRepository, ProvinceRepository>();

            services.AddTransient<ICityApplication, CityApplication>();
            services.AddTransient<ICityRepository, CityRepository>();

            services.AddTransient<IPictureCategoryApplication, PictureCategoryApplication>();
            services.AddTransient<IPictureCategoryRepository, PictureCategoryRepository>();

            services.AddTransient<ISlideApplication, SlideApplication>();
            services.AddTransient<ISlideRepository, SlideRepository>();

            services.AddTransient<ISlideQuery, SlideQuery>();
            services.AddTransient<IOrganizatonGroupQuery, OrganizationGroupQuery>();

            services.AddDbContext<BasicDataContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
