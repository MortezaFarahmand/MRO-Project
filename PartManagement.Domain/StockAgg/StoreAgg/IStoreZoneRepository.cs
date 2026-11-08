using _0_Framework.Domain;

namespace PartManagement.Domain.StockAgg.StoreAgg
{
    public interface IStoreZoneRepository : IRepository<long, StoreZone>
    {
        List<StoreZone> GetByStore(long storeId);
        StoreZone GetReceivingHoldZone(long storeId);
    }
}