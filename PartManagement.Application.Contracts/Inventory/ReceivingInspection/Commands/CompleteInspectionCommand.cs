using PartManagement.Domain.InventoryAgg.ReceivingInspectionAgg.Enum;

namespace PartManagement.Application.Contracts.Inventory.ReceivingInspection.Commands
{
    public class CompleteInspectionCommand
    {
        public long ResultId { get; set; }
        public InspectionStatus FinalStatus { get; set; } // Approved / Rejected / NeedsMoreInspection
        public string Notes { get; set; }
        public long InspectorUserId { get; set; }
    }
}
