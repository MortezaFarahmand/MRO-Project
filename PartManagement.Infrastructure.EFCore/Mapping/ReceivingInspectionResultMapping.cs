using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartManagement.Domain.InventoryAgg.ReceivingInspectionAgg;

namespace PartManagement.Infrastructure.EFCore.Mapping
{
    public class ReceivingInspectionResultMapping : IEntityTypeConfiguration<ReceivingInspectionResult>
    {
        public void Configure(EntityTypeBuilder<ReceivingInspectionResult> builder)
        {
            builder.ToTable("ReceivingInspectionResults");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Notes).HasMaxLength(2000);
            builder.Property(x => x.OverrideReason).HasMaxLength(1000);

            // فقط از روی پراپرتی عمومی Checks
            builder.HasMany(x => x.Checks)
                .WithOne()
                .HasForeignKey(x => x.ResultId)
                .OnDelete(DeleteBehavior.Cascade);

            // بگو از فیلد _checks بخوان/بنویس (نه از خود property)
            builder.Navigation(x => x.Checks)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasField("_checks");
        }
    }
}
