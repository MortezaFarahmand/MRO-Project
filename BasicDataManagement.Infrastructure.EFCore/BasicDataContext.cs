using BasicDataManagement.Domain.CityAgg;
using BasicDataManagement.Domain.CountryAgg;
using BasicDataManagement.Domain.EntitiAgg;
using BasicDataManagement.Domain.PictureCategoryAgg;
using BasicDataManagement.Domain.ProvinceAgg;
using BasicDataManagement.Domain.SlideAgg;
using BasicDataManagement.Infrastructure.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;
using OrganizationManagement.Domain.OrganizationGroupAgg;

namespace BasicDataManagement.Infrastructure.EFCore
{
    public class BasicDataContext : DbContext
    {
        public DbSet<Entiti> Entitis { get; set; }
        public DbSet<Country> Countrys { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Citys { get; set; }
        public DbSet<PictureCategory> PictureCategorys { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<OrganizationGroup> OrganizationGroups { get; set; }



        public BasicDataContext(DbContextOptions<BasicDataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(EntitiMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }

}
