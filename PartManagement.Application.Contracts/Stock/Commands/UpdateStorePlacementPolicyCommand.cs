using PartManagement.Domain.StockAgg.StoreAgg.Enum;

namespace PartManagement.Application.Contracts.Stock.Commands
{
    public class UpdateStorePlacementPolicyCommand
    {
        public long StoreId { get; set; }
        public PlacementPolicyMode Mode { get; set; }
        public bool UseAtaChapter { get; set; }
        public bool UseKeywordGroup { get; set; }
        public bool UseStockType { get; set; }
        public bool UseAircraftModel { get; set; }
        public bool UsePartPhysical { get; set; }
        public bool UseLocationKind { get; set; }
        public bool UseEnvironmentalClass { get; set; }
    }
}
