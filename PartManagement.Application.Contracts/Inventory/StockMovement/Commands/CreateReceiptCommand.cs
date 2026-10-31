namespace PartManagement.Application.Contracts.Inventory.StockMovement.Commands
{
    public class CreateReceiptCommand
    {
        public string MovementNumber { get; set; }
        public long PartId { get; set; }
        public long ToStoreId { get; set; }
        public decimal Quantity { get; set; }
        public string SerialNumber { get; set; }
        public string BatchNumber { get; set; }
        public long? InventoryItemId { get; set; }
        public long? OrganizationId { get; set; }
        public long? RelatedDocumentId { get; set; }
        public string RelatedDocumentType { get; set; }
        public string Note { get; set; }
    }
}
