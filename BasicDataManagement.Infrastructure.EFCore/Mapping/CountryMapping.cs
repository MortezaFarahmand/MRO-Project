using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BasicDataManagement.Domain.CountryAgg;

namespace BasicDatanManagement.Infrastructure.EFCore.Mapping
{
    public class CountryMapping : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countrys");
            builder.HasKey(x => x.Id);

            builder.Property(x=>x.Name).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Alpha2Code).HasMaxLength(100);
            builder.Property(x => x.Alpha3Code).HasMaxLength(100);
            builder.Property(x => x.UNCode).HasMaxLength(100);
            builder.Property(x => x.DialCode).HasMaxLength(100);
            builder.Property(x => x.PictureId);
            builder.Property(x => x.TailCode).HasMaxLength(100);
            builder.Property(x => x.MetaDescription).HasMaxLength(500);
            builder.Property(x => x.Slug).HasMaxLength(500);

            builder.HasMany(x => x.Provinces)
                .WithOne(x => x.Country)
                .HasForeignKey(x => x.CountryId);

            //builder.HasMany(x => x.OrganizationAviationCodes)
            //       .WithOne(x => x.Countries)
            //       .HasForeignKey(x => x.CountryId);

            //builder.HasOne(x => x.ApprovalAuthority)
            //       .WithOne(x => x.Country)
            //       .HasForeignKey<ApprovalAuthority>(x => x.CountryId);
        }
    }
}
