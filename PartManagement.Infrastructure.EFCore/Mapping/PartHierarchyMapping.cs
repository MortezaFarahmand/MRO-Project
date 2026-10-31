using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartManagement.Domain.PartAgg;

public class PartHierarchyMapping : IEntityTypeConfiguration<PartHierarchy>
{
    public void Configure(EntityTypeBuilder<PartHierarchy> builder)
    {
        builder.ToTable("PartHierarchies");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Quantity).HasPrecision(18, 4);
    }
}