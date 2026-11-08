using PartManagement.Domain.InventoryAgg.ReceivingInspectionAgg.Enum;

namespace PartManagement.Application.Contracts.Inventory.ReceivingInspection.DTOs
{
    public class ReceivingInspectionWizardDto
    {
        public long ResultId { get; set; }
        public long ReceiptId { get; set; }
        public long? InventoryItemId { get; set; }
        public long StoreId { get; set; }
        public string PartNumber { get; set; }
        public string SerialOrBatch { get; set; }
        public InspectionStatus Status { get; set; }
        public int CurrentStepIndex { get; set; }
        public int TotalSteps { get; set; }
        public int CompletedSteps { get; set; }
        public bool AllowsPlacement { get; set; }
        public bool IsOverridden { get; set; }
        public string OverrideReason { get; set; }
        public List<WizardStepDto> Steps { get; set; } = new();
    }

    public class WizardStepDto
    {
        public int Index { get; set; }
        public long InspectionItemId { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string HelpText { get; set; }
        public bool IsMandatory { get; set; }
        public bool AllowNotApplicable { get; set; }
        public CheckOutcome Outcome { get; set; }
        public string Remark { get; set; }
    }
}
