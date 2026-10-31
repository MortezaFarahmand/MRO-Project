using PartManagement.Domain.InventoryAgg.InventoryItemAgg.Enum;

namespace PartManagement.Application.Contracts.Inventory.InventoryItem.Queries
{
    public class SearchInventoryQuery
    {
        public long? PartId { get; set; }
        public long? StoreId { get; set; }
        public string SerialNumber { get; set; }
        public string BatchNumber { get; set; }
        public InventoryStatus? Status { get; set; }
    }
}
