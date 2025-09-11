using BasicDataManagement.Domain.CityAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasicDataManagement.Infrastructure.EFCore.Mapping
{
    public class CityMapping : IEntityTypeConfiguration<City> 
    {
        public void Configure(EntityTypeBuilder<City> builder) 
        {
            builder.ToTable("Citys");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(256).IsRequired();
            builder.Property(x => x.DialCode).HasMaxLength(256);
            builder.Property(x => x.MetaDescription).HasMaxLength(256);
            builder.Property(x => x.Slug).HasMaxLength(256);
            builder.Property(x => x.Keywords).HasMaxLength(256);
            builder.Property(x => x.DialCode).HasMaxLength(256);

            builder.HasOne(x => x.Province)
                .WithMany(x => x.Cities)
                .HasForeignKey(x => x.ProvinceId);
        }
    }
}
