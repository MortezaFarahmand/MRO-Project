using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartManagement.Domain.Stock.StockTypeAgg;

namespace PartManagement.Infrastructure.EFCore.Mapping
{
    public class StockTypeMapping : IEntityTypeConfiguration<StockType>
    {
        public void Configure(EntityTypeBuilder<StockType> builder)
        {
            builder.ToTable("StockTypes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Code).HasMaxLength(5).IsRequired();
            builder.Property(x => x.ColorCode).HasMaxLength(20);
            builder.Property(x => x.CreationDate).HasMaxLength(50);  
        }
    }
}
