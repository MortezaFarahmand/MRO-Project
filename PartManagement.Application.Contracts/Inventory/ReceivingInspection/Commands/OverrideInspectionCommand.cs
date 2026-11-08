
namespace PartManagement.Application.Contracts.Inventory.ReceivingInspection.Commands
{
    public class OverrideInspectionCommand
    {
        public long ResultId { get; set; }
        public long AuthorizedUserId { get; set; }
        public string Reason { get; set; }
    }
}
