namespace PartManagement.Application.Contracts.Inventory.StockMovement.DTOs
{
    public class StockMovementDto
    {
        public long Id { get; set; }
        public string MovementNumber { get; set; }
        public string MovementType { get; set; }
        public long PartId { get; set; }
        public string PartNumber { get; set; }
        public long? FromStoreId { get; set; }
        public long? ToStoreId { get; set; }
        public decimal Quantity { get; set; }
        public string SerialNumber { get; set; }
        public string BatchNumber { get; set; }
        public DateTime MovementDate { get; set; }
        public string Note { get; set; }
    }
}
