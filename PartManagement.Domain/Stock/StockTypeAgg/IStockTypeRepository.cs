using _0_Framework.Domain;
using PartManagement.Application.Contracts.Stock.StockType;

namespace PartManagement.Domain.Stock.StockTypeAgg
{
    public interface IStockTypeRepository : IRepository<long, StockType>
    {
        List<StockTypeViewModel> GetStockTypesBy(long? organizationId);
        EditStockType Getdetails(long id);
        List<StockTypeViewModel> Search(StockTypeSearchModel searchModel);

    }
}
