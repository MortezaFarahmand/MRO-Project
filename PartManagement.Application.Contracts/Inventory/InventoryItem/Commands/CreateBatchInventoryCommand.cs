namespace PartManagement.Application.Contracts.Inventory.InventoryItem.Commands
{
    public class CreateBatchInventoryCommand
    {
        public long PartId { get; set; }
        public string BatchNumber { get; set; }
        public decimal Quantity { get; set; }
        public long? StoreId { get; set; }
        public long? OwnerOrganizationId { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string Note { get; set; }
    }
}
