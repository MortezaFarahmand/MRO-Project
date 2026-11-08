using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartManagement.Domain.InventoryAgg.ReceivingInspectionAgg;

namespace PartManagement.Infrastructure.EFCore.Mapping
{
    public class ReceivingInspectionItemMapping : IEntityTypeConfiguration<ReceivingInspectionItem>
    {
        public void Configure(EntityTypeBuilder<ReceivingInspectionItem> builder)
        {
            builder.ToTable("ReceivingInspectionItems");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(250).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(1000);
            builder.Property(x => x.HelpText).HasMaxLength(1000);
            builder.Property(x => x.ApplicableKeyword).HasMaxLength(100);

            builder.HasIndex(x => x.Code).IsUnique();
        }
    }
}
