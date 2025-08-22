

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrganizationManagement.Domain.OrganizationPictureAgg;

namespace OrganizationManagement.Infrastructure.EFCore.Mapping
{
    public class OrganizationPictureMapping : IEntityTypeConfiguration<OrganizationPicture>
    {
        public void Configure(EntityTypeBuilder<OrganizationPicture> builder)
        {
            builder.ToTable("OrganizationPictures");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Picture).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.PictureAlt).HasMaxLength(500).IsRequired();
            builder.Property(x => x.PictureTitle).HasMaxLength(500).IsRequired();

            builder.HasOne(x => x.Organization)
                .WithMany(x => x.OrganizationPictures)
                .HasForeignKey(x => x.OrganizationId);  
        }
    }
}
