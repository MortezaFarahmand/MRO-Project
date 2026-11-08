using _0_Framework.Application;
using PartManagement.Application.Contracts.Stock.Commands;

namespace PartManagement.Application.Contracts.Stock.Store
{
    public interface IStoreApplication
    {
        OperationResult Create(CreateStore command);
        OperationResult Edit(EditStore command);
        EditStore GetDetails(long id);
        List<StoreViewModel> Search(StoreSearchModel searchModel);
        List<StoreViewModel> GetStoresBy(long? organizationId);
        OperationResult Activate(long id);
        OperationResult Deactivate(long id);
        //OperationResult AddRoom(List<AddRemoveStoreRoom> command);
        //OperationResult AddRoom(AddRemoveStoreRoom command);
        //OperationResult RemoveRoom(List<AddRemoveStoreRoom> command);
        //OperationResult RemoveRoom(AddRemoveStoreRoom command);
        OperationResult ForbidSearchOfStore(long id);
        OperationResult AllowSearchOfStore(long id);
        OperationResult SetAutoRequestOrder(long id);
        OperationResult DisableAutoRequestOrder(long id);

        OperationResult UpdateReceivingPolicy(UpdateStoreReceivingPolicyCommand command);
        OperationResult UpdatePlacementPolicy(UpdateStorePlacementPolicyCommand command);

    }
}
