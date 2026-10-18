using _0_Framework.Domain;
using PartManagement.Domain.Stock.StockTypeAgg;

namespace PartManagement.Domain.Stock.StoreAgg
{
    public class StoreStockType
    {
        public long StoreId { get; private set; }
        public Store Store { get; private set; }

        public long StockTypeId { get; private set; }
        public StockType StockType { get; private set; }

        public StoreStockType(long storeId, long stockTypeId)
        {
            StoreId = storeId;
            StockTypeId = stockTypeId;
        }
    }
}
