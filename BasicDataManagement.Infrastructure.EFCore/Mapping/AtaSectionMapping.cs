using BasicDataManagement.Domain.ATAChapterAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasicDataManagement.Infrastructure.EFCore.Mapping
{
    public class AtaSectionMapping : IEntityTypeConfiguration<AtaSection>
    {
        public void Configure(EntityTypeBuilder<AtaSection> builder)
        {
            builder.ToTable("AtaSections");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code).IsRequired().HasMaxLength(4);
            builder.Property(x => x.FullCode).IsRequired().HasMaxLength(10);
            builder.Property(x => x.TitleEn).IsRequired().HasMaxLength(200);
            builder.Property(x => x.TitleFa).HasMaxLength(200);
            builder.HasIndex(x => x.FullCode).IsUnique();

            builder.HasMany(x => x.Subjects)
                   .WithOne(s => s.Section)
                   .HasForeignKey(s => s.AtaSectionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
