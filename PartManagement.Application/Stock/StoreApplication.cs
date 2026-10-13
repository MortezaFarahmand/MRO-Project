using _0_Framework.Application;
using PartManagement.Application.Contracts.Stock.Store;
using PartManagement.Domain.Stock.StoreAgg;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PartManagement.Application.Stock
{
    public class StoreApplication : IStoreApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly IStoreRepository _storeRepository;

        public StoreApplication(IStoreRepository storeRepository, IAuthHelper authHelper)
        {
            _storeRepository = storeRepository;
            _authHelper = authHelper;
        }


        public OperationResult Create(CreateStore command)
        {
            var operation = new OperationResult();
            var currentOrganizationId = _authHelper.CurrentAccountOrganizationId;

            if (_storeRepository.Exists(x => (x.Name == command.Name || x.Code == command.Code)
                 && x.OrganizationId == command.OrganizationId))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var store = new Store(command.Name, command.NameFa, command.Code, command.ColorCode,
                command.Description, command.OrganizationId, command.CityId, command.StationId,
                command.StockTypeId, command.ParentId, command.ProviderId, command.DefaultValueOfStockMovement);

            _storeRepository.Create(store);
            _storeRepository.SaveChanges();
            return operation.Succeeded();

        }

        public OperationResult Edit(EditStore command)
        {
            var operation = new OperationResult();
            var store = _storeRepository.Get(command.Id);
            if (store == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if(_storeRepository.Exists(x => x.Id != command.Id && 
            (x.Name == command.Name || x.Code == command.Code) &&
                    x.OrganizationId == command.OrganizationId ) )
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            store.Edit(command.Name, command.NameFa, command.Code, command.ColorCode,
                command.Description, command.OrganizationId, command.CityId, command.StationId,
                command.StockTypeId, command.ParentId, command.ProviderId, 
                command.DefaultValueOfStockMovement);
            
            _storeRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditStore GetDetails(long id)
        {
            return _storeRepository.GetDetails(id);
        }

        public List<StoreViewModel> Search(StoreSearchModel searchModel)
        {
            return _storeRepository.Search(searchModel);
        }

        public OperationResult ForbidSearchOfStore(long id)
        {
            var operation = new OperationResult();
            var store = _storeRepository.Get(id);
            if (store == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            store.ForbidSearchOfStore();
            _storeRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult AllowSearchOfStore(long id)
        {
            var operation = new OperationResult();
            var store = _storeRepository.Get(id);
            if (store == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            store.AllowSearchOfStore();
            _storeRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Activate(long id)
        {
            var operation = new OperationResult();
            var store = _storeRepository.Get(id);
            if (store == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            store.Active();
            _storeRepository.SaveChanges();
            return operation.Succeeded();
        }
        public OperationResult Deactivate(long id)
        {
            var operation = new OperationResult();
            var store = _storeRepository.Get(id);
            if (store == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            store.Deactive();
            _storeRepository.SaveChanges();
            return operation.Succeeded();
        }

        //public OperationResult AddRoom(List<AddRemoveStoreRoom> command)
        //{
        //    throw new NotImplementedException();
        //}

        //public OperationResult RemoveRoom(List<AddRemoveStoreRoom> command)
        //{
        //    throw new NotImplementedException();
        //}

        public OperationResult SetAutoRequestOrder(long id)
        {
            var operation = new OperationResult();
            var store = _storeRepository.Get(id);
            if (store == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            store.SetAutoRequestOrder();
            _storeRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult DisableAutoRequestOrder(long id)
        {
            var operation = new OperationResult();
            var store = _storeRepository.Get(id);
            if (store == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            store.DisableAutoRequestOrder();
            _storeRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<StoreViewModel> GetStoresByOrganizationId(StoreViewModel storeViewModel)
        {
            long currentOrganizationId = _authHelper.CurrentAccountOrganizationId();

            return _storeRepository.GetStoresByOrganizationId(storeViewModel, currentOrganizationId);
        }

        //public OperationResult AddRoom(AddRemoveStoreRoom command)
        //{
        //    var operation = new OperationResult();
        //    var store = _storeRepository.Get(command.StockTypeId);
        //    if (store == null)
        //        return operation.Failed(ApplicationMessages.RecordNotFound);

        //    store.AddRoom(stockType);
        //    _storeRepository.SaveChanges();
        //    return operation.Succeeded();
        //}

        //public OperationResult RemoveRoom(AddRemoveStoreRoom command)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
