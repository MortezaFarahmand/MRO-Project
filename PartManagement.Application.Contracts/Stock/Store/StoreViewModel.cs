using PartManagement.Application.Contracts.Stock.StockType;
using PartManagement.Domain.StockAgg.StoreAgg.Enum;

namespace PartManagement.Application.Contracts.Stock.Store
{
    public class StoreViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? OrganizationName { get; set; }
        public string City { get; set; }
        public string Station { get; set; }
        public string StockType { get; set; }
        public string Parent { get; set; }
        public string Provider { get; set; }
        public long StockTypeId { get; set; }
        public long? OrganizationId { get; set; }
        public string CreationDate { get; set; }

        public bool IsActive { get; set; }
        public List<StockTypeViewModel> StockTypes { get; set; } = new();
        public long CityId { get; set; }
        public long StationId { get; set; }
        public long ParentId { get; set; }

        // روی EditStore یا StoreViewModel جزئیات
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
