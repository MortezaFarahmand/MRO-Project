using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonnelManagement.Domain.PersonGroupAgg;
using System;

namespace PersonnelManagement.Infrastructure.EFCore.Mapping
{
    public class PersonGroupMapping : IEntityTypeConfiguration<PersonGroup>
    {
        public void Configure(EntityTypeBuilder<PersonGroup> builder)
        {
            builder.ToTable("PersonGroups");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Remark).HasMaxLength(500);
            builder.Property(x => x.IsActive);
        }
    }
}
