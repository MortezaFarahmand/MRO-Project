using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartManagement.Domain.InventoryAgg.StockMovementAgg;

namespace PartManagement.Infrastructure.EFCore.Mapping
{
    public class StockMovementMapping : IEntityTypeConfiguration<StockMovement>
    {
        public void Configure(EntityTypeBuilder<StockMovement> builder)
        {
            builder.ToTable("StockMovements");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.MovementNumber).HasMaxLength(50).IsRequired();
            builder.Property(x => x.SerialNumber).HasMaxLength(50);
            builder.Property(x => x.BatchNumber).HasMaxLength(50);
            builder.Property(x => x.RelatedDocumentType).HasMaxLength(50);
            builder.Property(x => x.Note).HasMaxLength(1000);
            builder.Property(x => x.Quantity).HasPrecision(18, 4);

            builder.HasIndex(x => x.MovementNumber);
            builder.HasIndex(x => x.PartId);
            builder.HasIndex(x => x.MovementDate);

            builder.HasOne(x => x.Part)
                .WithMany()
                .HasForeignKey(x => x.PartId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}