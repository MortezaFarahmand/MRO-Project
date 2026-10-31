using _0_Framework.Infrastructure;
using PartManagement.Domain.InventoryAgg.InventoryItemAgg;
using PartManagement.Domain.InventoryAgg.InventoryItemAgg.Enum;

namespace PartManagement.Infrastructure.EFCore.Repository
{
    public class InventoryItemRepository : RepositoryBase<long, InventoryItem>, IInventoryItemRepository
    {
        private readonly PartContext _context;

        public InventoryItemRepository(PartContext context) : base(context)
        {
            _context = context;
        }

        public InventoryItem GetBySerialNumber(string serialNumber)
        {
            return _context.InventoryItems
                .FirstOrDefault(x => x.SerialNumber == serialNumber.Trim().ToUpper());
        }

        public List<InventoryItem> GetByPartId(long partId)
        {
            return _context.InventoryItems
                .Where(x => x.PartId == partId)
                .OrderByDescending(x => x.Id)
                .ToList();
        }

        public List<InventoryItem> GetByStoreId(long storeId)
        {
            return _context.InventoryItems
                .Where(x => x.StoreId == storeId)
                .OrderByDescending(x => x.Id)
                .ToList();
        }

        public bool ExistsSerialNumber(string serialNumber)
        {
            return _context.InventoryItems
                .Any(x => x.SerialNumber == serialNumber.Trim().ToUpper());
        }

        public List<InventoryItem> Search(
            long? partId,
            long? storeId,
            string serialNumber,
            string batchNumber,
            InventoryStatus? status)
        {
            var query = _context.InventoryItems.AsQueryable();

            if (partId.HasValue)
                query = query.Where(x => x.PartId == partId.Value);

            if (storeId.HasValue)
                query = query.Where(x => x.StoreId == storeId.Value);

            if (!string.IsNullOrWhiteSpace(serialNumber))
                query = query.Where(x => x.SerialNumber.Contains(serialNumber.Trim().ToUpper()));

            if (!string.IsNullOrWhiteSpace(batchNumber))
                query = query.Where(x => x.BatchNumber.Contains(batchNumber.Trim().ToUpper()));

            if (status.HasValue)
                query = query.Where(x => x.Status == status.Value);

            return query
                .OrderByDescending(x => x.Id)
                .ToList();
        }
    }
}