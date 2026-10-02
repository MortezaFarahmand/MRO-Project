using BasicDataManagement.Domain.AirportAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasicDataManagement.Infrastructure.EFCore.Mapping
{
    public class AirportMapping : IEntityTypeConfiguration<Airport>
    {
        public void Configure(EntityTypeBuilder<Airport> builder)
        {
            builder.ToTable("Airports");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Ident).HasMaxLength(256);
            builder.Property(x => x.Type).HasMaxLength(256);
            builder.Property(x => x.Name).HasMaxLength(256);
            builder.Property(x => x.Latitude_deg);
            builder.Property(x => x.Longitude_deg);
            builder.Property(x => x.Elevation_ft);
            builder.Property(x => x.Continent).HasMaxLength(50);
            builder.Property(x => x.Iso_country).HasMaxLength(50);
            builder.Property(x => x.Iso_region).HasMaxLength(50);
            builder.Property(x => x.Municipality).HasMaxLength(256);
            builder.Property(x => x.Scheduled_service);
            builder.Property(x => x.Icao_code).HasMaxLength(50);
            builder.Property(x => x.Iata_code).HasMaxLength(50);
            builder.Property(x => x.Gps_code).HasMaxLength(50);
            builder.Property(x => x.Local_code).HasMaxLength(50);
            builder.Property(x => x.Home_link).HasMaxLength(500);
            builder.Property(x => x.Wikipedia_link).HasMaxLength(500);
            builder.Property(x => x.Keywords).HasMaxLength(500);
        }
    }
}
