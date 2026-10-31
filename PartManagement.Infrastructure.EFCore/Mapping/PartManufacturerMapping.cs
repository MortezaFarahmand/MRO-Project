using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartManagement.Domain.PartAgg;

public class PartManufacturerMapping : IEntityTypeConfiguration<PartManufacturer>
{
    public void Configure(EntityTypeBuilder<PartManufacturer> builder)
    {
        builder.ToTable("PartManufacturers");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Note).HasMaxLength(512);
    }
}