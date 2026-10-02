using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartManagement.Domain.Basic_data.EquipmentTypeAgg;

namespace PartManagement.Infrastructure.EFCore.Mapping
{
    public class EquipmentTypeMapping : IEntityTypeConfiguration<EquipmentType>
    {
        public void Configure(EntityTypeBuilder<EquipmentType> builder)
        {
            builder.ToTable("EquipmentTypes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Code).HasMaxLength(5).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.ColorCode).HasMaxLength(20);
            builder.Property(x => x.CreationDate).HasMaxLength(50);
        }
    }
}
