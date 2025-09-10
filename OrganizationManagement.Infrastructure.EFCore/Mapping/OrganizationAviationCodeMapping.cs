using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrganizationManagement.Domain.OrganizationAviationCodeAgg;

namespace OrganizationManagement.Infrastructure.EFCore.Mapping
{
    public class OrganizationAviationCodeMapping : IEntityTypeConfiguration<OrganizationAviationCode>
    {
        public void Configure(EntityTypeBuilder<OrganizationAviationCode> builder)
        {
            builder.ToTable("OrganizationAviationCodes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ICAO).HasMaxLength(20).IsRequired();
            builder.Property(x => x.IATA).HasMaxLength(20);
            builder.Property(x => x.CivilAutority).HasMaxLength(255);
            builder.Property(x => x.Comment).HasMaxLength(500);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.CallSign).HasMaxLength(100);
            builder.Property(x => x.CountryId).HasMaxLength(255);
            builder.Property(x => x.IsEnabled);

            //builder.HasOne(x => x.Countries)
            //    .WithMany(x => x.OrganizationAviationCodes)
            //    .HasForeignKey(x => x.CountryId);
        }
    }
}
