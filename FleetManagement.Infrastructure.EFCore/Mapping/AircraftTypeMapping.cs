using FleetManagement.Domain.Basic_dataAgg.AircraftTypeAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.Infrastructure.EFCore.Mapping
{
    public class AircraftTypeMapping : IEntityTypeConfiguration<AircraftType>
    {
        public void Configure(EntityTypeBuilder<AircraftType> builder)
        {
            builder.ToTable("AircraftTypes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500);

            builder.HasIndex(x => x.Code).IsUnique();
        }
    }
}
