using _0_Framework.Application;
using _01_MROQuery.Contracts.City;
using _01_MROQuery.Contracts.Organization;
using _01_MROQuery.Contracts.Station;
using PartManagement.Application.Contracts.Stock.Commands;
using PartManagement.Application.Contracts.Stock.DTOs;
using PartManagement.Application.Contracts.Stock.StockType;
using PartManagement.Application.Contracts.Stock.Store;
using PartManagement.Domain.Stock.StoreAgg;
using PartManagement.Domain.StockAgg.StoreAgg;

namespace PartManagement.Application.Stock
{
    public class StoreApplication : IStoreApplication
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IAuthHelper _authHelper;
        private readonly IOrganizationQuery _organizationQuery;   // برای گرفتن نام سازمان
        private readonly IStoreZoneRepository _storeZoneRepository;
        private readonly ICityQuery _cityQuery;
        private readonly IStationQuery _stationQuery;

        public StoreApplication(
            IStoreRepository storeRepository,
            IAuthHelper authHelper,
            IOrganizationQuery organizationQuery,
            IStoreZoneRepository storeZoneRepository,
            ICityQuery cityQuery, IStationQuery stationQuery)
        {
            _storeRepository = storeRepository;
            _authHelper = authHelper;
            _organizationQuery = organizationQuery;
            _storeZoneRepository = storeZoneRepository;
            _cityQuery = cityQuery;
            _stationQuery = stationQuery;
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

            store.EnsureDefaultPolicies();
            _storeRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Edit(EditStore command)
        {
            var operation = new OperationResult();
            var store = _storeRepository.GetWithDetails(command.Id);

            if (store == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_storeRepository.Exists(x =>
                x.Id != command.Id &&
                (x.Name == command.Name || x.Code == command.Code) &&
                x.OrganizationId == command.OrganizationId))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            long? storeOrganizationId = _authHelper.SetOrganizationId(command.OrganizationId);

            // به‌روزرسانی انواع مجاز انبار
            store.SetStockTypes(command.StockTypeIds ?? new List<long>());

            store.Edit(
                command.Name,
                command.NameFa,
                command.Code,
                command.ColorCode,
                command.Description,
                storeOrganizationId,
                command.CityId,
                command.StationId,
                command.StockTypeId,       // این DefaultStockTypeId است
                command.ParentId,         
                command.ProviderId,
                command.DefaultValueOfStockMovement);

            _storeRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditStore GetDetails(long id)
        {
            var store = _storeRepository.GetWithDetails(id);
            if (store == null)
                return null;

            string organizationName = null;
            if (store.OrganizationId.HasValue)
                organizationName = _organizationQuery.GetNameEn(store.OrganizationId.Value);

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
                OrganizationName = organizationName,
                CityId = store.CityId,
                StationId = store.StationId,
                StockTypeId = store.DefaultStockTypeId,
                ParentId = store.ParentId,
                ProviderId = store.ProviderId,
                StockTypeIds = store.StoreStockTypes?
                    .Select(x => x.StockTypeId).ToList() ?? new List<long>(),
                NotToSearchBySpecificStore = store.NotToSearchBySpecificStore,
                AutomaticRequestOrder = store.AutomaticRequestOrder,

                ReceivingInspectionMode = store.ReceivingPolicy?.InspectionMode,
                InspectionDeadlineHours = store.ReceivingPolicy?.InspectionDeadlineHours,
                AllowOneStepReceiveAndInspect = store.ReceivingPolicy?.AllowOneStepReceiveAndInspect,
                DefaultReceivingHoldZoneId = store.ReceivingPolicy?.DefaultReceivingHoldZoneId,

                PlacementMode = store.PlacementPolicy?.Mode,
                UseAtaChapter = store.PlacementPolicy?.UseAtaChapter,
                UseKeywordGroup = store.PlacementPolicy?.UseKeywordGroup,
                UseStockType = store.PlacementPolicy?.UseStockType,
                UseAircraftModel = store.PlacementPolicy?.UseAircraftModel,
                UsePartPhysical = store.PlacementPolicy?.UsePartPhysical,
                UseLocationKind = store.PlacementPolicy?.UseLocationKind,
                UseEnvironmentalClass = store.PlacementPolicy?.UseEnvironmentalClass,

                StockTypes = new List<StockTypeViewModel>(),
                Organizations = new List<OrganizationSimpleDto>()
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
                // نام شهر
                string cityName = null;
                if (store.CityId > 0)
                    cityName = _cityQuery.GetDisplay(store.CityId);   // یا متد مناسب خودت

                // نام ایستگاه
                string stationIata_code = null;
                if (store.StationId > 0)
                    stationIata_code = _stationQuery.GetDisplay(store.StationId);  // یا متد مناسب خودت

                // نام انبار والد
                string parentName = null;
                if (store.ParentId > 0)
                {
                    var parent = stores.FirstOrDefault(x => x.Id == store.ParentId)
                                 ?? _storeRepository.Get(store.ParentId);
                    parentName = parent?.Name;
                }

                result.Add(new StoreViewModel
                {
                    Id = store.Id,
                    Name = store.Name,
                    Code = store.Code,
                    OrganizationId = store.OrganizationId,
                    OrganizationName = organizationName,     // نام سازمان
                    City = cityName,
                    Station = stationIata_code,
                    StockTypeId = store.DefaultStockTypeId,
                    StockType = store.DefaultStockType?.Name,
                    Parent = parentName,
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

        public OperationResult UpdateReceivingPolicy(UpdateStoreReceivingPolicyCommand command)
        {
            var result = new OperationResult();
            var store = _storeRepository.Get(command.StoreId);

            if (!_authHelper.CurrentAccountIsAdmin()
                && store.OrganizationId != _authHelper.CurrentAccountOrganizationId())
                return result.Failed("دسترسی مجاز نیست.");

            if (store == null) return result.Failed("انبار یافت نشد.");

            if (command.DefaultReceivingHoldZoneId.HasValue)
            {
                var zone = _storeZoneRepository.Get(command.DefaultReceivingHoldZoneId.Value);
                if (zone == null || zone.StoreId != command.StoreId || !zone.IsReceivingHold)
                    return result.Failed("Zone نگهداری موقت نامعتبر است.");
            }

            store.SetReceivingPolicy(
                command.InspectionMode,
                command.InspectionDeadlineHours,
                command.AllowOneStepReceiveAndInspect,
                command.DefaultReceivingHoldZoneId);

            _storeRepository.SaveChanges();
            return result.Succeeded("سیاست دریافت ذخیره شد.");
        }

        public OperationResult UpdatePlacementPolicy(UpdateStorePlacementPolicyCommand command)
        {
            var result = new OperationResult();
            var store = _storeRepository.Get(command.StoreId);

            if (!_authHelper.CurrentAccountIsAdmin()
                && store.OrganizationId != _authHelper.CurrentAccountOrganizationId())
                return result.Failed("دسترسی مجاز نیست.");

            if (store == null) return result.Failed("انبار یافت نشد.");

            store.SetPlacementPolicy(
                command.Mode,
                command.UseAtaChapter,
                command.UseKeywordGroup,
                command.UseStockType,
                command.UseAircraftModel,
                command.UsePartPhysical,
                command.UseLocationKind,
                command.UseEnvironmentalClass);

            _storeRepository.SaveChanges();
            return result.Succeeded("سیاست چیدمان ذخیره شد.");
        }

        public List<StoreViewModel> GetStoresBy(long? organizationId)
        {
            var orgId = _authHelper.SetOrganizationId(organizationId);

            var stores = _storeRepository.Search(
                name: null,
                code: null,
                organizationId: orgId,
                isActive: true);

            return stores.Select(store => new StoreViewModel
            {
                Id = store.Id,
                Name = store.Name,
                Code = store.Code,
                OrganizationId = store.OrganizationId,
                OrganizationName = store.OrganizationId.HasValue
                    ? _organizationQuery.GetNameEn(store.OrganizationId.Value)
                    : null,
                IsActive = store.IsActive
            }).ToList();
        }

        public List<StoreSimpleDto> GetStoresForParent(long? organizationId, long? excludeStoreId = null)
        {
            var orgId = _authHelper.SetOrganizationId(organizationId);

            var stores = _storeRepository.Search(
                name: null,
                code: null,
                organizationId: orgId,
                isActive: true);

            return stores
                .Where(x => !excludeStoreId.HasValue || x.Id != excludeStoreId.Value)
                .Select(x => new StoreSimpleDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    DefaultStockType = x.DefaultStockType?.Name
                })
                .OrderBy(x => x.Name)
                .ToList();
        }
    }
}