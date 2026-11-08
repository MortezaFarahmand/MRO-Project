using PartManagement.Domain.StockAgg.StoreAgg.Enum;

namespace PartManagement.Application.Contracts.Stock.Store
{
    public class EditStore : CreateStore
    {
        public long Id { get; set; }
        public InspectionMode? ReceivingInspectionMode { get; set; }
        public int? InspectionDeadlineHours { get; set; }
        public bool? AllowOneStepReceiveAndInspect { get; set; }
        public long? DefaultReceivingHoldZoneId { get; set; }

        public PlacementPolicyMode? PlacementMode { get; set; }
        public bool? UseAtaChapter { get; set; }
        public bool? UseKeywordGroup { get; set; }
        public bool? UseStockType { get; set; }
        public bool? UseAircraftModel { get; set; }
        public bool? UsePartPhysical { get; set; }
        public bool? UseLocationKind { get; set; }
        public bool? UseEnvironmentalClass { get; set; }
    }
}