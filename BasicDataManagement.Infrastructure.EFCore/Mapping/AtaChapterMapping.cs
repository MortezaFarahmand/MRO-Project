using BasicDataManagement.Domain.ATAChapterAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace BasicDataManagement.Infrastructure.EFCore.Mapping
{
    public class AtaChapterMapping : IEntityTypeConfiguration<AtaChapter>
    {
        public void Configure(EntityTypeBuilder<AtaChapter> builder)
        {
            builder.ToTable("AtaChapters");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).IsRequired().HasMaxLength(4);
            builder.HasIndex(x => x.Code).IsUnique();               // ← همان Index روی Code

            builder.Property(x => x.TitleEn).IsRequired().HasMaxLength(200);
            builder.Property(x => x.TitleFa).HasMaxLength(200);
            builder.Property(x => x.Description).HasMaxLength(1000);
            builder.Property(x => x.Group).HasConversion<int>();

            builder.HasMany(x => x.Sections)
                   .WithOne(s => s.Chapter)
                   .HasForeignKey(s => s.AtaChapterId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
