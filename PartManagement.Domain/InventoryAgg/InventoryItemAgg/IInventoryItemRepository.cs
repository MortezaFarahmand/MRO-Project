using _0_Framework.Domain;
using PartManagement.Domain.InventoryAgg.InventoryItemAgg.Enum;

namespace PartManagement.Domain.InventoryAgg.InventoryItemAgg
{
    public interface IInventoryItemRepository : IRepository<long, InventoryItem>
    {
        InventoryItem GetBySerialNumber(string serialNumber);
        List<InventoryItem> GetByPartId(long partId);
        List<InventoryItem> GetByStoreId(long storeId);
        List<InventoryItem> Search(long? partId, long? storeId, string serialNumber, string batchNumber, InventoryStatus? status);
        bool ExistsSerialNumber(string serialNumber);
    }
}
