using BasicDataManagement.Domain.ATAChapterAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace BasicDataManagement.Infrastructure.EFCore.Mapping
{
    public class AtaSubjectMapping : IEntityTypeConfiguration<AtaSubject>
    {
        public void Configure(EntityTypeBuilder<AtaSubject> builder)
        {
            builder.ToTable("AtaSubjects");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code).IsRequired().HasMaxLength(4);
            builder.Property(x => x.FullCode).IsRequired().HasMaxLength(15);
            builder.Property(x => x.TitleEn).IsRequired().HasMaxLength(200);
            builder.Property(x => x.TitleFa).HasMaxLength(200);
            builder.Property(x => x.WdmChapterSection).HasMaxLength(15);
            builder.Property(x => x.IpcChapterSection).HasMaxLength(15);
            builder.HasIndex(x => x.FullCode).IsUnique();
        }
    }
}
