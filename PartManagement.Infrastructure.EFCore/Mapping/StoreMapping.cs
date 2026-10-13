using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartManagement.Domain.Stock.StoreAgg;

namespace PartManagement.Infrastructure.EFCore.Mapping
{
    public class StoreMapping : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.ToTable("Stores");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(256).IsRequired();
            builder.Property(x => x.NameFa).HasMaxLength(256);
            builder.Property(x => x.Code).HasMaxLength(10).IsRequired();
            builder.Property(x => x.ColorCode).HasMaxLength(20);
            builder.Property(x => x.Description).HasMaxLength(500);

            builder.HasOne(x => x.StockType)
                .WithMany(x => x.Stores)
                .HasForeignKey(x => x.StockTypeId);
        }
    }
}
