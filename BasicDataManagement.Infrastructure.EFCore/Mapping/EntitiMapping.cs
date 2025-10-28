using BasicDataManagement.Domain.EntitiAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasicDataManagement.Infrastructure.EFCore.Mapping
{
    public class EntitiMapping : IEntityTypeConfiguration<Entiti>
    {
        public void Configure(EntityTypeBuilder<Entiti> builder)
        {
            builder.ToTable("Entitis");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(256);
            builder.Property(x => x.Remark).HasMaxLength(500);
            builder.Property(x => x.IsActive);



        }
    }
}
