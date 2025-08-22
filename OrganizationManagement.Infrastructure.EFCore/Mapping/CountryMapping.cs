using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrganizationManagement.Domain.ApprovalAutorityAgg;
using OrganizationManagement.Domain.CountryAgg;

namespace OrganizationManagement.Infrastructure.EFCore.Mapping
{
    public class CountryMapping : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countrys");
            builder.HasKey(x => x.Id);

            builder.Property(x=>x.Name).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Alpha2Code).HasMaxLength(100);
            builder.Property(x => x.Alpha3Code).HasMaxLength(100);
            builder.Property(x => x.UNCode).HasMaxLength(100);
            builder.Property(x => x.DialCode).HasMaxLength(100);
            builder.Property(x => x.Picture).HasMaxLength(1000);
            builder.Property(x => x.TailCode).HasMaxLength(100);


            builder.HasMany(x => x.OrganizationAviationCodes)
                   .WithOne(x => x.Countries)
                   .HasForeignKey(x => x.CountryId);

            builder.HasOne(x => x.ApprovalAuthority)
                   .WithOne(x => x.Country)
                   .HasForeignKey<ApprovalAuthority>(x => x.CountryId);
        }
    }
}
