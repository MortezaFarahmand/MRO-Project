namespace PartManagement.Application.Contracts.Inventory.InventoryItem.Commands
{
    public class QuarantineInventoryCommand
    {
        public long Id { get; set; }
        public string Reason { get; set; }
    }
}
