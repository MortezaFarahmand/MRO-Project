using _0_Framework.Application;
using PartManagement.Application.Contracts.Inventory.StockMovement.Commands;
using PartManagement.Application.Contracts.Inventory.StockMovement.DTOs;
using PartManagement.Application.Contracts.Inventory.StockMovement.Queries;

namespace PartManagement.Application.Contracts.Inventory.StockMovement
{
    public interface IStockMovementApplication
    {
        OperationResult CreateReceipt(CreateReceiptCommand command);
        OperationResult CreateIssue(CreateIssueCommand command);
        OperationResult CreateTransfer(CreateTransferCommand command);

        StockMovementDto GetById(long id);
        List<StockMovementDto> Search(SearchStockMovementQuery query);
        List<StockMovementDto> GetByPartId(long partId);
    }
}
