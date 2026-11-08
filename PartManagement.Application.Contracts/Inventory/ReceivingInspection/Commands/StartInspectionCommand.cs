
namespace PartManagement.Application.Contracts.Inventory.ReceivingInspection.Commands
{
    public class StartInspectionCommand
    {
        public long ResultId { get; set; }
        public long InspectorUserId { get; set; }
    }
}
