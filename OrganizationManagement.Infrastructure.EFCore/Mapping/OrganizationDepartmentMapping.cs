using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrganizationManagement.Domain.OrganizationDepartmentAgg;

namespace OrganizationManagement.Infrastructure.EFCore.Mapping
{
    public class OrganizationDepartmentMapping : IEntityTypeConfiguration<OrganizationDepartment>
    {
        public void Configure(EntityTypeBuilder<OrganizationDepartment> builder)
        {
            builder.ToTable("OrganizationDepartments");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(100000);
            builder.Property(x => x.MetaDescription).HasMaxLength(100000);
            builder.Property(x => x.Slug).HasMaxLength(500);


            builder.HasOne(x => x.Organization)
                .WithMany(x => x.OrganizationDepartments)
                .HasForeignKey(x => x.OrganizationId);
        }
    }
}
