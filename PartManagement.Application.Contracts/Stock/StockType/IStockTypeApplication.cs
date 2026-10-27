using _0_Framework.Application;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace PartManagement.Application.Contracts.Stock.StockType
{
    public interface IStockTypeApplication
    {
        OperationResult Create(CreateStockType command);
        OperationResult Edit(EditStockType command);
        EditStockType GetDetails(long id);
        //CreateStockType GetStockTypesBy();

        List<StockTypeViewModel> GetStockTypesBy(long? organizationId);
        List<StockTypeViewModel> Search(StockTypeSearchModel searchModel);

    }
}
