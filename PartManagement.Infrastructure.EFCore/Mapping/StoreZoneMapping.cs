using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartManagement.Domain.StockAgg.StoreAgg;

namespace PartManagement.Infrastructure.EFCore.Mapping
{
    public class StoreZoneMapping : IEntityTypeConfiguration<StoreZone>
    {
        public void Configure(EntityTypeBuilder<StoreZone> builder)
        {
            builder.ToTable("StoreZones");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.KeywordGroup).HasMaxLength(100);

            builder.HasIndex(x => new { x.StoreId, x.Code }).IsUnique();
        }
    }
}
