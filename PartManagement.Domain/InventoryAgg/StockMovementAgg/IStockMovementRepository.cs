using _0_Framework.Domain;
using PartManagement.Domain.InventoryAgg.StockMovementAgg.Enums;

namespace PartManagement.Domain.InventoryAgg.StockMovementAgg
{
    public interface IStockMovementRepository : IRepository<long, StockMovement>
    {
        StockMovement GetByMovementNumber(string movementNumber);
        List<StockMovement> GetByPartId(long partId);
        List<StockMovement> GetByInventoryItemId(long inventoryItemId);
        List<StockMovement> Search(string movementNumber, long? partId, long? storeId, 
            StockMovementType? type, DateTime? fromDate, DateTime? toDate);
    }
}
