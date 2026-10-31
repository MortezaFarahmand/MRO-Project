using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartManagement.Domain.InventoryAgg.InventoryItemAgg;

namespace PartManagement.Infrastructure.EFCore.Mapping
{
    public class InventoryItemMapping : IEntityTypeConfiguration<InventoryItem>
    {
        public void Configure(EntityTypeBuilder<InventoryItem> builder)
        {
            builder.ToTable("InventoryItems");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.SerialNumber).HasMaxLength(50);
            builder.Property(x => x.BatchNumber).HasMaxLength(50);
            builder.Property(x => x.LotNumber).HasMaxLength(50);
            builder.Property(x => x.Note).HasMaxLength(1000);
            builder.Property(x => x.Quantity).HasPrecision(18, 4);
            builder.Property(x => x.FlightHoursAtInstall).HasPrecision(18, 2);

            builder.HasIndex(x => x.SerialNumber);
            builder.HasIndex(x => x.BatchNumber);
            builder.HasIndex(x => new { x.PartId, x.Status });

            builder.HasOne(x => x.Part)
                .WithMany()
                .HasForeignKey(x => x.PartId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}