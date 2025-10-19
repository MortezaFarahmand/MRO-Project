using BasicDataManagement.Domain.ProvinceAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasicDataManagement.Infrastructure.EFCore.Mapping
{
    public class ProvinceMapping : IEntityTypeConfiguration<Province> 
    {
        public void Configure(EntityTypeBuilder<Province> builder) 
        {
            builder.ToTable("Provinces");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(256).IsRequired();
            builder.Property(x => x.DialCode).HasMaxLength(256);
            builder.Property(x => x.MetaDescription).HasMaxLength(256);
            builder.Property(x => x.Slug).HasMaxLength(256);
            builder.Property(x => x.Keywords).HasMaxLength(256);
            builder.Property(x => x.DialCode).HasMaxLength(256);

            builder.HasOne(x => x.Country)
                .WithMany(x => x.Provinces)
                .HasForeignKey(x => x.CountryId);

            builder.HasMany(x => x.Cities)
                .WithOne(x => x.Province)
                .HasForeignKey(x => x.ProvinceId);
        }
    }
}
