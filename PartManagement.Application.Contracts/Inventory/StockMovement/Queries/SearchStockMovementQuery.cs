using PartManagement.Domain.InventoryAgg.StockMovementAgg.Enums;

namespace PartManagement.Application.Contracts.Inventory.StockMovement.Queries
{
    public class SearchStockMovementQuery
    {
        public string MovementNumber { get; set; }
        public long? PartId { get; set; }
        public long? StoreId { get; set; }
        public StockMovementType? MovementType { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
