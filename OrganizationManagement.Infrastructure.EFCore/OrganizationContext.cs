using Microsoft.EntityFrameworkCore;
using BasicDatanManagement.Domain.ApprovalAutorityAgg;
using BasicDatanManagement.Domain.OrganizationAgg;
using BasicDatanManagement.Domain.OrganizationAviationCodeAgg;
using BasicDatanManagement.Domain.OrganizationGroupAgg;
using BasicDatanManagement.Domain.OrganizationPictureAgg;
using BasicDatanManagement.Domain.SlideAgg;
using BasicDatanManagement.Infrastructure.EFCore.Mapping;

namespace BasicDatanManagement.Infrastructure.EFCore
{
    public class OrganizationContext : DbContext
    {
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationGroup> OrganizationGroups { get; set; }
        public DbSet<OrganizationPicture> OrganizationPictures { get; set; }
        public DbSet<Slide> Slides { get; set; }
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
