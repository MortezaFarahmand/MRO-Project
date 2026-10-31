using _0_Framework.Application;
using PartManagement.Application.Contracts.Inventory.InventoryItem.Commands;
using PartManagement.Application.Contracts.Inventory.InventoryItem.DTOs;
using PartManagement.Application.Contracts.Inventory.InventoryItem.Queries;

namespace PartManagement.Application.Contracts.Inventory.InventoryItem
{
    public interface IInventoryItemApplication
    {
        OperationResult CreateSerialized(CreateSerializedInventoryCommand command);
        OperationResult CreateBatch(CreateBatchInventoryCommand command);
        OperationResult Allocate(long id);
        OperationResult Install(InstallInventoryCommand command);
        OperationResult ReturnToStock(ReturnToStockCommand command);
        OperationResult SendForRepair(long id);
        OperationResult Quarantine(QuarantineInventoryCommand command);
        OperationResult Scrap(ScrapInventoryCommand command);

        InventoryItemDto GetById(long id);
        InventoryItemDto GetBySerialNumber(string serialNumber);
        List<InventoryItemDto> Search(SearchInventoryQuery query);
    }
}
