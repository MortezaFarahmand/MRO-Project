using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartManagement.Domain.PartAgg;

namespace PartManagement.Infrastructure.EFCore.Mapping
{
    public class PartUtilisationMapping : IEntityTypeConfiguration<PartUtilisation>
    {
        public void Configure(EntityTypeBuilder<PartUtilisation> builder)
        {
            builder.ToTable("PartUtilisations");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.IsPrimary).IsRequired();
            builder.Property(x => x.Note).HasMaxLength(500);

            builder.HasOne(x => x.Part)
                   .WithMany(x => x.Utilisations)
                   .HasForeignKey(x => x.PartId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.PrimaryUtilisation)
                   .WithMany()
                   .HasForeignKey(x => x.PrimaryUtilisationId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.PartId, x.PrimaryUtilisationId }).IsUnique();
        }
    }
}