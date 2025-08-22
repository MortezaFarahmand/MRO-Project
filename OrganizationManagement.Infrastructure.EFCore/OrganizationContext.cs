using Microsoft.EntityFrameworkCore;
using OrganizationManagement.Domain.ApprovalAutorityAgg;
using OrganizationManagement.Domain.CountryAgg;
using OrganizationManagement.Domain.OrganizationAgg;
using OrganizationManagement.Domain.OrganizationAviationCodeAgg;
using OrganizationManagement.Domain.OrganizationGroupAgg;
using OrganizationManagement.Domain.OrganizationPictureAgg;
using OrganizationManagement.Domain.SlideAgg;
using OrganizationManagement.Infrastructure.EFCore.Mapping;

namespace OrganizationManagement.Infrastructure.EFCore
{
    public class OrganizationContext : DbContext
    {
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationGroup> OrganizationGroups { get; set; }
        public DbSet<OrganizationPicture> OrganizationPictures { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<Country> Countrys { get; set; }
        public DbSet<OrganizationAviationCode> OrganizationAviationCodes { get; set; }
        public DbSet<ApprovalAuthority> ApprovalAuthorities { get; set; }

        public OrganizationContext(DbContextOptions<OrganizationContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(OrganizationGroupMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }

}
