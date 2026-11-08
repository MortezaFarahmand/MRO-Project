using Microsoft.AspNetCore.Mvc.Rendering;
using PartManagement.Domain.StockAgg.StoreAgg.Enum;

namespace PartManagement.Application.Contracts.Stock.Commands
{
    public class CreateStoreZoneCommand
    {
        public long StoreId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ZoneType ZoneType { get; set; }
        public int? AtaChapterId { get; set; }
        public string? KeywordGroup { get; set; }
        public long? AircraftModelId { get; set; }
        public LocationKind? LocationKind { get; set; }
        public EnvironmentalClass? EnvironmentalClass { get; set; }
        public decimal? MaxWeightKg { get; set; }
        public decimal? MaxVolumeM3 { get; set; }
        public string StoreName { get; set; }

        // فقط برای UI:
        public SelectList Stores { get; set; }
        public SelectList ZoneTypes { get; set; }
        public SelectList LocationKinds { get; set; }
        public SelectList EnvironmentalClasses { get; set; }
        public SelectList AircraftModels { get; set; }
        public SelectList AtaChapters { get; set; }
    }
}
