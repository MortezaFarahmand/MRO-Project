using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrganizationManagement.Domain.OrganizationPositionAgg;

namespace OrganizationManagement.Infrastructure.EFCore.Mapping
{
    public class OrganizationPositionMapping : IEntityTypeConfiguration<OrganizationPosition>
    {
        public void Configure(EntityTypeBuilder<OrganizationPosition> builder)
        {
            builder.ToTable("OrganizationPositions");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(100000);
            builder.Property(x => x.MetaDescription).HasMaxLength(100000);
            builder.Property(x => x.Slug).HasMaxLength(500);


            builder.HasOne(x => x.OrganizationDepartment)
                .WithMany(x => x.OrganizationPositions)
                .HasForeignKey(x => x.OrganizationDepartmentId);
        }
    }
}
