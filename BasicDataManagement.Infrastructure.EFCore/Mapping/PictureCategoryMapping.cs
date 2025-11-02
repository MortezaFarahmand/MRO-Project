using BasicDataManagement.Domain.PictureCategoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BasicDataManagement.Infrastructure.EFCore.Mapping
{
    public class PictureCategoryMapping : IEntityTypeConfiguration<PictureCategory>
    {
        public void Configure(EntityTypeBuilder<PictureCategory> builder)
        {
            builder.ToTable("PictureCategorys");
            builder.HasKey(p => p.Id);

            builder.Property(x => x.Name).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Remark).HasMaxLength(500);

            //builder.HasMany(x => x.Provinces)
            //    .WithOne(x => x.Country)
            //    .HasForeignKey(x => x.CountryId);
        }
    }
}
