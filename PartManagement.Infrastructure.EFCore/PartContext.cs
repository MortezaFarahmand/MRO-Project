using Microsoft.EntityFrameworkCore;
using PartManagement.Domain.Basic_data.EquipmentTypeAgg;
using PartManagement.Domain.Stock.StockTypeAgg;
using PartManagement.Infrastructure.EFCore.Mapping;

namespace PartManagement.Infrastructure.EFCore
{
    public class PartContext : DbContext
    {
        public DbSet<StockType> StockTypes { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }

        public PartContext(DbContextOptions<PartContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(StockTypeMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
