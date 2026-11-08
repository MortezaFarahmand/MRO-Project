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

            builder.HasOne(x => x.DefaultStockType)
                .WithMany(x => x.Stores_DefaultStockType)
                .HasForeignKey(x => x.DefaultStockTypeId)
                .OnDelete(DeleteBehavior.Restrict); // خیلی مهم;

            //Store Policies//
            builder.OwnsOne(x => x.ReceivingPolicy, rp =>
            {
                rp.Property(p => p.InspectionMode).HasColumnName("Receiving_InspectionMode");
                rp.Property(p => p.InspectionDeadlineHours).HasColumnName("Receiving_DeadlineHours");
                rp.Property(p => p.AllowOneStepReceiveAndInspect).HasColumnName("Receiving_AllowOneStep");
                rp.Property(p => p.DefaultReceivingHoldZoneId).HasColumnName("Receiving_HoldZoneId");
                // مهم: StoreId را map نکنید
                rp.Ignore(p => p.StoreId);
            });

            builder.OwnsOne(x => x.PlacementPolicy, pp =>
            {
                pp.Property(p => p.Mode).HasColumnName("Placement_Mode");
                pp.Property(p => p.UseAtaChapter).HasColumnName("Placement_UseAta");
                pp.Property(p => p.UseKeywordGroup).HasColumnName("Placement_UseKeyword");
                pp.Property(p => p.UseStockType).HasColumnName("Placement_UseStockType");
                pp.Property(p => p.UseAircraftModel).HasColumnName("Placement_UseAircraftModel");
                pp.Property(p => p.UsePartPhysical).HasColumnName("Placement_UsePartPhysical");
                pp.Property(p => p.UseLocationKind).HasColumnName("Placement_UseLocationKind");
                pp.Property(p => p.UseEnvironmentalClass).HasColumnName("Placement_UseEnvClass");
                // مهم: StoreId را map نکنید
                pp.Ignore(p => p.StoreId);
            });
        }
    }
}
