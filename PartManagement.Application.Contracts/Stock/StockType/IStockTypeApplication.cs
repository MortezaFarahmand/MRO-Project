using _0_Framework.Application;

namespace PartManagement.Application.Contracts.Stock.StockType
{
    public interface IStockTypeApplication
    {
        OperationResult Create(CreateStockType command);
        OperationResult Edit(EditStockType command);
        EditStockType GetDetails(long id);
        List<StockTypeViewModel> GetStockTypes();
        List<StockTypeViewModel> Search(StockTypeSearchModel searchModel);

    }
}
