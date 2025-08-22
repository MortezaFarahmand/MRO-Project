using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrganizationManagement.Domain.OrganizationGroupAgg;

namespace OrganizationManagement.Infrastructure.EFCore.Mapping
{
    public class OrganizationGroupMapping : IEntityTypeConfiguration<OrganizationGroup>
    {
        public void Configure(EntityTypeBuilder<OrganizationGroup> builder)
        {
            builder.ToTable("OrganizationGroups");
            builder.HasKey(x => x.Id);

            builder.Property(x=>x.Name).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(100000).IsRequired();
            builder.Property(x=>x.Picture).HasMaxLength(1000);
            builder.Property(x => x.NameCode).HasMaxLength(20).IsRequired();
            builder.Property(x => x.PictureAlt).HasMaxLength(255);
            builder.Property(x => x.PictureTitle).HasMaxLength(500);
            builder.Property(x => x.Keywords).HasMaxLength(100);
            builder.Property(x => x.MetaDescription).HasMaxLength(100000);
            builder.Property(x => x.Slug).HasMaxLength(500);

            builder.HasMany(x=>x.Organizations)
                .WithOne(x=>x.Group)
                .HasForeignKey(x=>x.OrganizationGroupId);
        }
    }
}
