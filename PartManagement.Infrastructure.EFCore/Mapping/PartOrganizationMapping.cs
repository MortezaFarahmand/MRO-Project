using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartManagement.Domain.PartAgg;

public class PartOrganizationMapping : IEntityTypeConfiguration<PartOrganization>
{
    public void Configure(EntityTypeBuilder<PartOrganization> builder)
    {
        builder.ToTable("PartOrganizations");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Note).HasMaxLength(512);

        builder.HasIndex(x => new { x.PartId, x.OrganizationId }).IsUnique();
    }
}