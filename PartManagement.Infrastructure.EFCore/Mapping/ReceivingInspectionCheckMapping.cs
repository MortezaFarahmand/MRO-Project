using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartManagement.Domain.InventoryAgg.ReceivingInspectionAgg;

namespace PartManagement.Infrastructure.EFCore.Mapping
{
    public class ReceivingInspectionCheckMapping : IEntityTypeConfiguration<ReceivingInspectionCheck>
    {
        public void Configure(EntityTypeBuilder<ReceivingInspectionCheck> builder)
        {
            builder.ToTable("ReceivingInspectionChecks");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Remark).HasMaxLength(1000);
            builder.Property(x => x.ResultId).IsRequired();
            builder.Property(x => x.InspectionItemId).IsRequired();
            builder.Property(x => x.StepIndex).IsRequired();

            builder.HasOne<ReceivingInspectionItem>()
                .WithMany()
                .HasForeignKey(x => x.InspectionItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
