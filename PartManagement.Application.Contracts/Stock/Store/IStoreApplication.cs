using _0_Framework.Application;

namespace PartManagement.Application.Contracts.Stock.Store
{
    public interface IStoreApplication 
    {
        OperationResult Create(CreateStore command);
        OperationResult Edit(EditStore command);
        EditStore GetDetails(long id);
        List<StoreViewModel> GetStoresByOrganizationId(StoreViewModel storeViewModel);
        List<StoreViewModel> Search(StoreSearchModel searchModel);
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

    }
}
