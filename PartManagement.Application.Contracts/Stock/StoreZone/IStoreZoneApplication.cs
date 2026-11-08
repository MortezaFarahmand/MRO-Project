using _0_Framework.Application;
using PartManagement.Application.Contracts.Stock.Commands;
using PartManagement.Application.Contracts.Stock.DTOs;

namespace PartManagement.Application.Contracts.Stock.StoreZone
{
    public interface IStoreZoneApplication
    {
        OperationResult Create(CreateStoreZoneCommand command);
        OperationResult Edit(EditStoreZoneCommand command);
        OperationResult Activate(long id);
        OperationResult Deactivate(long id);
        StoreZoneDto GetDetails(long id);
        List<StoreZoneDto> GetByStore(long storeId);
        List<StoreZoneDto> Search(long? storeId, string code, bool? isActive);
    }
}
