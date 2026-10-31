namespace PartManagement.Application.Contracts.Inventory.InventoryItem.Commands
{
    public class ScrapInventoryCommand
    {
        public long Id { get; set; }
        public string Reason { get; set; }
    }
}
