using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Domain.PersonGroupAgg;
using PersonnelManagement.Infrastructure.EFCore.Mapping;

namespace PersonnelnManagement.Infrastructure.EFCore
{
    public class PersonnelContext : DbContext
    {
        public DbSet<PersonGroup> PersonGroups { get; set; }

        public PersonnelContext(DbContextOptions<PersonnelContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(PersonGroupMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }

}
