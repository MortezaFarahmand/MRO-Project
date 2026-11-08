using PartManagement.Domain.StockAgg.StoreAgg.Enum;

namespace PartManagement.Application.Contracts.Stock.DTOs
{
    public class StoreZoneDto
    {
        public long Id { get; set; }
        public long StoreId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ZoneType ZoneType { get; set; }
        public string ZoneTypeTitle { get; set; }
        public int? AtaChapterId { get; set; }
        public string? AtaChapterDisplay { get; set; } // از BasicData در Query
        public string KeywordGroup { get; set; }
        public long? AircraftModelId { get; set; }
        public LocationKind? LocationKind { get; set; }
        public EnvironmentalClass? EnvironmentalClass { get; set; }
        public decimal? MaxWeightKg { get; set; }
        public decimal? MaxVolumeM3 { get; set; }
        public bool IsActive { get; set; }
        public string? StoreName { get; set; }
    }
}
