namespace PartManagement.Application.Contracts.Inventory.InventoryItem.Commands
{
    public class ReturnToStockCommand
    {
        public long Id { get; set; }
        public long StoreId { get; set; }
    }
}
