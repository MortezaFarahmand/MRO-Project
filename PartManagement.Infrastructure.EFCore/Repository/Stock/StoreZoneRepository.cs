using _0_Framework.Infrastructure;
using PartManagement.Domain.StockAgg.StoreAgg;
using PartManagement.Domain.StockAgg.StoreAgg.Enum;

namespace PartManagement.Infrastructure.EFCore.Repository.Stock
{
    public class StoreZoneRepository
        : RepositoryBase<long, StoreZone>, IStoreZoneRepository
    {
        private readonly PartContext _context;

        public StoreZoneRepository(PartContext context) : base(context)
        {
            _context = context;
        }

        public List<StoreZone> GetByStore(long storeId)
        {
            return _context.StoreZones
                .Where(x => x.StoreId == storeId)
                .OrderBy(x => x.Code)
                .ToList();
        }

        public StoreZone GetReceivingHoldZone(long storeId)
        {
            return _context.StoreZones
                .FirstOrDefault(x => x.StoreId == storeId
                                     && x.IsActive
                                     && (x.ZoneType == ZoneType.ReceivingHold
                                         || x.ZoneType == ZoneType.Quarantine));
        }
    }
}
