using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartManagement.Domain.PartAgg;

namespace PartManagement.Infrastructure.EFCore.Mapping
{
    public class PartAlternateMapping : IEntityTypeConfiguration<PartAlternate>
    {
        public void Configure(EntityTypeBuilder<PartAlternate> builder)
        {
            builder.ToTable("PartAlternates");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.RelationType).HasMaxLength(20).IsRequired();
            builder.Property(x => x.AlternateCategory).HasMaxLength(50);
            builder.Property(x => x.Notes).HasMaxLength(500);

            builder.HasOne(x => x.PrimaryPart)
                .WithMany()
                .HasForeignKey(x => x.PrimaryPartId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AlternatePart)
                .WithMany()
                .HasForeignKey(x => x.AlternatePartId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.PrimaryPartId, x.AlternatePartId }).IsUnique();
        }
    }
}