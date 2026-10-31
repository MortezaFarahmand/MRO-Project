using _0_Framework.Infrastructure;
using PartManagement.Domain.InventoryAgg.StockMovementAgg;
using PartManagement.Domain.InventoryAgg.StockMovementAgg.Enums;

namespace PartManagement.Infrastructure.EFCore.Repository
{
    public class StockMovementRepository : RepositoryBase<long, StockMovement>, IStockMovementRepository
    {
        private readonly PartContext _context;

        public StockMovementRepository(PartContext context) : base(context)
        {
            _context = context;
        }

        public StockMovement GetByMovementNumber(string movementNumber)
        {
            return _context.StockMovements
                .FirstOrDefault(x => x.MovementNumber == movementNumber);
        }

        public List<StockMovement> GetByPartId(long partId)
        {
            return _context.StockMovements
                .Where(x => x.PartId == partId)
                .OrderByDescending(x => x.MovementDate)
                .ToList();
        }

        public List<StockMovement> GetByInventoryItemId(long inventoryItemId)
        {
            return _context.StockMovements
                .Where(x => x.InventoryItemId == inventoryItemId)
                .OrderByDescending(x => x.MovementDate)
                .ToList();
        }

        public List<StockMovement> Search(
            string movementNumber,
            long? partId,
            long? storeId,
            StockMovementType? type,
            DateTime? fromDate,
            DateTime? toDate)
        {
            var query = _context.StockMovements.AsQueryable();

            if (!string.IsNullOrWhiteSpace(movementNumber))
                query = query.Where(x => x.MovementNumber.Contains(movementNumber));

            if (partId.HasValue)
                query = query.Where(x => x.PartId == partId.Value);

            if (storeId.HasValue)
                query = query.Where(x => x.FromStoreId == storeId.Value || x.ToStoreId == storeId.Value);

            if (type.HasValue)
                query = query.Where(x => x.MovementType == type.Value);

            if (fromDate.HasValue)
                query = query.Where(x => x.MovementDate >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(x => x.MovementDate <= toDate.Value);

            return query
                .OrderByDescending(x => x.MovementDate)
                .ToList();
        }
    }
}