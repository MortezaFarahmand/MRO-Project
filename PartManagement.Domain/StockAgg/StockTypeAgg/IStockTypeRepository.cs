//using PartManagement.Application.Contracts.Stock.StockType;

//namespace PartManagement.Domain.Stock.StockTypeAgg
//{
//    public interface IStockTypeRepository : IRepository<long, StockType>
//    {
//        List<StockTypeViewModel> GetStockTypesBy(long? organizationId);
//        EditStockType Getdetails(long id);
//        List<StockTypeViewModel> Search(StockTypeSearchModel searchModel);

//    }
//}
using _0_Framework.Domain;
using PartManagement.Domain.Stock.StockTypeAgg;

namespace PartManagement.Domain.Stock.StockTypeAgg
{
    public interface IStockTypeRepository : IRepository<long, StockType>
    {
        List<StockType> GetByOrganizationId(long? organizationId);
        List<StockType> Search(string name, long? organizationId);
    }
}