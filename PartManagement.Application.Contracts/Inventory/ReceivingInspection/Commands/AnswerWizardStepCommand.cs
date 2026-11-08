using PartManagement.Domain.InventoryAgg.ReceivingInspectionAgg.Enum;

namespace PartManagement.Application.Contracts.Inventory.ReceivingInspection.Commands
{
    public class AnswerWizardStepCommand
    {
        public long ResultId { get; set; }
        public int StepIndex { get; set; }
        public CheckOutcome Outcome { get; set; }
        public string Remark { get; set; }
        public long UserId { get; set; }
    }
}
