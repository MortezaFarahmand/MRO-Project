using FleetManagement.Domain.Basic_dataAgg.AircraftModelAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.Infrastructure.EFCore.Mapping
{
    public class AircraftModelMapping : IEntityTypeConfiguration<AircraftModel>
    {
        public void Configure(EntityTypeBuilder<AircraftModel> builder)
        {
            builder.ToTable("AircraftModels");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Manufacturer).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500);

            builder.HasIndex(x => x.Code).IsUnique();

            builder.HasOne(x => x.AircraftType)
                .WithMany()
                .HasForeignKey(x => x.AircraftTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
