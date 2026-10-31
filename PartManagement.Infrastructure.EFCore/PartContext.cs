using Microsoft.EntityFrameworkCore;
using PartManagement.Domain.Basic_data.EquipmentTypeAgg;
using PartManagement.Domain.InventoryAgg.InventoryItemAgg;
using PartManagement.Domain.InventoryAgg.StockMovementAgg;
using PartManagement.Domain.PartAgg;
using PartManagement.Domain.Stock.StockTypeAgg;
using PartManagement.Domain.Stock.StoreAgg;
using PartManagement.Infrastructure.EFCore.Mapping;

namespace PartManagement.Infrastructure.EFCore
{
    public class PartContext : DbContext
    {
        public DbSet<StockType> StockTypes { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreStockType> StoreStockTypes { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }

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
