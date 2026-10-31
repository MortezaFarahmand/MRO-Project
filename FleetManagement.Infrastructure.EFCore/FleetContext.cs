using FleetManagement.Domain.AircraftAgg;
using FleetManagement.Domain.Basic_dataAgg.AircraftModelAgg;
using FleetManagement.Domain.Basic_dataAgg.AircraftTypeAgg;
using FleetManagement.Infrastructure.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace FleetManagement.Infrastructure.EFCore
{
    public class FleetContext : DbContext
    {
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<AircraftModel> AircraftModels { get; set; }
        public DbSet<AircraftType> AircraftTypes { get; set; }

        public FleetContext(DbContextOptions<FleetContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(AircraftMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}