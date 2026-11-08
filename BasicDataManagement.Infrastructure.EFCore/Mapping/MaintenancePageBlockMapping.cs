using BasicDataManagement.Domain.ATAChapterAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasicDataManagement.Infrastructure.EFCore.Mapping
{
    public class MaintenancePageBlockMapping : IEntityTypeConfiguration<MaintenancePageBlock>
    {
        public void Configure(EntityTypeBuilder<MaintenancePageBlock> builder)
        {
            builder.ToTable("MaintenancePageBlocks");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.HasIndex(x => x.Code)
                   .IsUnique();

            builder.Property(x => x.TitleEn)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(x => x.TitleFa)
                   .HasMaxLength(200);

            builder.Property(x => x.StartPage)
                   .IsRequired();

            builder.Property(x => x.EndPage)
                   .IsRequired();

            builder.Property(x => x.DisplayOrder)
                   .IsRequired();
        }
    }
}
