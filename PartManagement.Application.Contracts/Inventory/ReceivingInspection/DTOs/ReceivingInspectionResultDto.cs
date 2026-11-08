using PartManagement.Domain.InventoryAgg.ReceivingInspectionAgg.Enum;

namespace PartManagement.Application.Contracts.Inventory.ReceivingInspection.DTOs
{
    public class ReceivingInspectionResultDto
    {
        public long Id { get; set; }
        public long ReceiptId { get; set; }
        public long? InventoryItemId { get; set; }
        public long StoreId { get; set; }
        public InspectionStatus Status { get; set; }
        public string StatusTitle { get; set; }
        public int TotalSteps { get; set; }
        public int CompletedSteps { get; set; }
        public DateTime? DeadlineAt { get; set; }
        public DateTime? InspectedAt { get; set; }
        public bool IsOverridden { get; set; }
        public string OverrideReason { get; set; }
        public bool AllowsPlacement { get; set; }
        public string Notes { get; set; }
    }
}
