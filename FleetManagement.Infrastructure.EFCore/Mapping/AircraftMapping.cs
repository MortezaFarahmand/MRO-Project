using FleetManagement.Domain.AircraftAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.Infrastructure.EFCore.Mapping
{
    public class AircraftMapping : IEntityTypeConfiguration<Aircraft>
    {
        public void Configure(EntityTypeBuilder<Aircraft> builder)
        {
            builder.ToTable("Aircrafts");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Registration).HasMaxLength(20).IsRequired();
            builder.Property(x => x.SerialNumber).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LineNumber).HasMaxLength(30);
            builder.Property(x => x.VariableNumber).HasMaxLength(30);
            builder.Property(x => x.Note).HasMaxLength(1000);

            builder.Property(x => x.TotalFlightHours).HasPrecision(18, 2);

            builder.HasIndex(x => x.Registration).IsUnique();
            builder.HasIndex(x => x.SerialNumber).IsUnique();

            builder.HasOne(x => x.AircraftModel)
                .WithMany()
                .HasForeignKey(x => x.AircraftModelId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}