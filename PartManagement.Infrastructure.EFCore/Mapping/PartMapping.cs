using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartManagement.Domain.PartAgg;

namespace PartManagement.Infrastructure.EFCore.Mapping
{
    public class PartMapping : IEntityTypeConfiguration<Part>
    {
        public void Configure(EntityTypeBuilder<Part> builder)
        {
            builder.ToTable("Parts");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PartNumber).HasMaxLength(64).IsRequired();
            builder.Property(x => x.Revision).HasMaxLength(32);
            builder.Property(x => x.Description).HasMaxLength(512);
            builder.Property(x => x.Keyword).HasMaxLength(256);
            builder.Property(x => x.Specification).HasMaxLength(512);
            builder.Property(x => x.Note).HasMaxLength(1024);

            builder.Property(x => x.ControlType).IsRequired();
            builder.Property(x => x.IsGlobal).IsRequired();

            // Indexes
            builder.HasIndex(x => x.PartNumber).IsUnique();

            // Relations
            builder.HasMany(x => x.Manufacturers)
                .WithOne()
                .HasForeignKey(x => x.PartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Utilisations)
                .WithOne()
                .HasForeignKey(x => x.PartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Organizations)
                .WithOne()
                .HasForeignKey(x => x.PartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Children)
                .WithOne(x => x.ParentPart)
                .HasForeignKey(x => x.ParentPartId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Parents)
                .WithOne(x => x.ChildPart)
                .HasForeignKey(x => x.ChildPartId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}