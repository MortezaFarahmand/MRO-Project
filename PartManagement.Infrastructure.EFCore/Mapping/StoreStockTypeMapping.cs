using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartManagement.Domain.Stock.StoreAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartManagement.Infrastructure.EFCore.Mapping
{
    public class StoreStockTypeMapping : IEntityTypeConfiguration<StoreStockType>
    {
        public void Configure(EntityTypeBuilder<StoreStockType> builder)
        {
            builder.ToTable("StoreStockTypes");
            builder.HasKey(x => new { x.StoreId, x.StockTypeId });

            builder.HasOne(x => x.Store)
                .WithMany(x => x.StoreStockTypes)
                .HasForeignKey(x => x.StoreId);
            builder.HasOne(x => x.StockType)
                .WithMany(x => x.StoreStockTypes)
                .HasForeignKey(x => x.StockTypeId);
        }
    }
}
