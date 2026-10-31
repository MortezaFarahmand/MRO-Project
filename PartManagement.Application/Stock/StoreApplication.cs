using _0_Framework.Application;
using _01_MROQuery.Contracts.Organization;
using PartManagement.Application.Contracts.Stock.Store;
using PartManagement.Domain.Stock.StoreAgg;

namespace PartManagement.Application.Stock
{
    public class StoreApplication : IStoreApplication
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IAuthHelper _authHelper;
        private readonly IOrganizationQuery _organizationQuery;   // برای گرفتن نام سازمان

        public StoreApplication(
            IStoreRepository storeRepository,
            IAuthHelper authHelper,
            IOrganizationQuery organizationQuery)
        {
            _storeRepository = storeRepository;
            _authHelper = authHelper;
            _organizationQuery = organizationQuery;
        }

        public OperationResult Create(CreateStore command)
        {
            var operation = new OperationResult();

            if (_storeRepository.Exists(x =>
                (x.Name == command.Name || x.Code == command.Code) &&
                x.OrganizationId == command.OrganizationId))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            long? storeOrganizationId = _authHelper.SetOrganizationId(command.OrganizationId);

            var store = new Store(
                command.Name,
                command.NameFa,
                command.Code,
                command.ColorCode,
                command.Description,
                storeOrganizationId,
                command.CityId,
                command.StationId,
                command.StockTypeId,
                command.ParentId,
                command.ProviderId,
                command.DefaultValueOfStockMovement);

            foreach (var stockTypeId in command.StockTypeIds)
            {
                var storeStockType = new StoreStockType(store.Id, stockTypeId);
                store.AddRooms(storeStockType);
            }

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

            if (_storeRepository.Exists(x =>
                x.Id != command.Id &&
                (x.Name == command.Name || x.Code == command.Code) &&
                x.OrganizationId == command.OrganizationId))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            long? storeOrganizationId = _authHelper.SetOrganizationId(command.OrganizationId);

            var storeStockTypes = command.StockTypeIds
                .Select(stockTypeId => new StoreStockType(store.Id, stockTypeId))
                .ToList();

            store.SetRooms(storeStockTypes);

            store.Edit(
                command.Name,
                command.NameFa,
                command.Code,
                command.ColorCode,
                command.Description,
                storeOrganizationId,
                command.CityId,
                command.StationId,
                command.StockTypeId,
                command.ParentId,
                command.ProviderId,
                command.DefaultValueOfStockMovement);

            _storeRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditStore GetDetails(long id)
        {
            var store = _storeRepository.Get(id);
            if (store == null)
                return null;

            var organizationName = store.OrganizationId.HasValue
                ? _organizationQuery.GetNameEn(store.OrganizationId.Value)
                : null;

            return new EditStore
            {
                Id = store.Id,
                Name = store.Name,
                NameFa = store.NameFa,
                Code = store.Code,
                ColorCode = store.ColorCode,
                Description = store.Description,
                DefaultValueOfStockMovement = store.DefaultValueOfStockMovement,
                OrganizationId = store.OrganizationId,
                OrganizationName = organizationName,          // نام سازمان
                CityId = store.CityId,
                StationId = store.StationId,
                StockTypeId = store.DefaultStockTypeId,
                ParentId = store.ParentId,
                ProviderId = store.ProviderId,
                StockTypeIds = store.StoreStockTypes?
                    .Select(x => x.StockTypeId)
                    .ToList() ?? new List<long>()
            };
        }

        public List<StoreViewModel> Search(StoreSearchModel searchModel)
        {
            searchModel ??= new StoreSearchModel();

            long? currentOrganizationId = _authHelper.SetOrganizationId(searchModel.OrganizationId);
            searchModel.IsAdmin = _authHelper.CurrentAccountIsAdmin();

            if (!searchModel.IsAdmin)
                searchModel.OrganizationId = currentOrganizationId;

            var stores = _storeRepository.Search(
                searchModel.Name,
                searchModel.Code,
                searchModel.OrganizationId,
                searchModel.IsActive);

            var result = new List<StoreViewModel>();

            foreach (var store in stores)
            {
                var organizationName = store.OrganizationId.HasValue
                    ? _organizationQuery.GetNameEn(store.OrganizationId.Value)
                    : null;

                result.Add(new StoreViewModel
                {
                    Id = store.Id,
                    Name = store.Name,
                    Code = store.Code,
                    OrganizationId = store.OrganizationId,
                    OrganizationName = organizationName,     // نام سازمان
                    CityId = store.CityId,
                    StationId = store.StationId,
                    StockTypeId = store.DefaultStockTypeId,
                    ParentId = store.ParentId,
                    IsActive = store.IsActive,
                    CreationDate = store.CreationDate.ToShortDateString()
                });
            }

            return result;
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
    }
}