using _0_Framework.Domain;
using PartManagement.Domain.Stock.StockTypeAgg;
using PartManagement.Domain.StockAgg.StoreAgg;
using PartManagement.Domain.StockAgg.StoreAgg.Enum;

namespace PartManagement.Domain.Stock.StoreAgg
{
    public class Store : EntityBase
    {
        public string Name { get; private set; }
        public string NameFa { get; private set; }
        public string Code { get; private set; }
        public string ColorCode { get; private set; }
        public string Description { get; private set; }
        public string DefaultValueOfStockMovement { get; private set; }
        public long? OrganizationId { get; private set; }
        public long CityId { get; private set; }
        public long StationId { get; private set; }
        public long DefaultStockTypeId { get; private set; }
        public StockType DefaultStockType { get; private set; }
        public long ParentId { get; private set; }
        public long ProviderId { get; private set; }
        public bool NotToSearchBySpecificStore { get; private set; }
        public bool AutomaticRequestOrder { get; private set; }
        public bool IsActive { get; private set; }
        public ICollection<StoreStockType> StoreStockTypes { get; private set; } = new List<StoreStockType>();
        // داخل PartManagement.Domain/StockAgg/StoreAgg/Store.cs  (اضافه به کلاس فعلی)

        public StoreReceivingPolicy ReceivingPolicy { get; private set; }
        public StorePlacementPolicy PlacementPolicy { get; private set; }


        public Store()
        {
            //StoreRooms = new List<StoreRoom>();
        }

        public Store(string name, string nameFa, string code, string colorCode,
            string description, long? organizationId, long cityId, long stationId, long stockTypeId,
            long parentId, long providerId, string defaultValueOfStockMovement)
        {
            Name = name;
            NameFa = nameFa;
            Code = code;
            ColorCode = colorCode;
            Description = description;
            OrganizationId = organizationId;
            CityId = cityId;
            StationId = stationId;
            DefaultStockTypeId = stockTypeId;
            ParentId = parentId;
            ProviderId = providerId;
            DefaultValueOfStockMovement = defaultValueOfStockMovement;
            IsActive = true;
            NotToSearchBySpecificStore = false;
            AutomaticRequestOrder = false;

        }

        public void Edit(string name, string nameFa, string code, string colorCode,
            string description, long? organizationId, long cityId, long stationId, long defaultStockTypeId,
            long parentId, long providerId, string defaultValueOfStockMovement)
        {
            Name = name;
            NameFa = nameFa;
            Code = code;
            ColorCode = colorCode;
            Description = description;
            OrganizationId = organizationId;
            CityId = cityId;
            StationId = stationId;
            DefaultStockTypeId = defaultStockTypeId;
            ParentId = parentId;
            ProviderId = providerId;
            DefaultValueOfStockMovement = defaultValueOfStockMovement;
        }

        public void AddRooms(StoreStockType storeStockType)
        {
            StoreStockTypes.Add(storeStockType);
        }
        public void SetRooms(List<StoreStockType> storeStockTypes)
        {
            StoreStockTypes.Clear();
            foreach (var st in storeStockTypes)
                StoreStockTypes.Add(st);
        }
        public void SetStockTypes(List<long> stockTypeIds)
        {
            stockTypeIds ??= new List<long>();
            stockTypeIds = stockTypeIds.Distinct().ToList();

            // ۱. حذف مواردی که دیگر انتخاب نشده‌اند
            var toRemove = StoreStockTypes
                .Where(x => !stockTypeIds.Contains(x.StockTypeId))
                .ToList();

            foreach (var item in toRemove)
            {
                StoreStockTypes.Remove(item);
            }

            // ۲. فقط مواردی را اضافه کن که هنوز وجود ندارند
            foreach (var stockTypeId in stockTypeIds)
            {
                if (!StoreStockTypes.Any(x => x.StockTypeId == stockTypeId))
                {
                    StoreStockTypes.Add(new StoreStockType(Id, stockTypeId));
                }
            }
        }

        public void ForbidSearchOfStore()
        {
            NotToSearchBySpecificStore = true;
        }

        public void AllowSearchOfStore()
        {
            NotToSearchBySpecificStore = false;
        }

        public void SetAutoRequestOrder()
        {
            AutomaticRequestOrder = true;
        }

        public void DisableAutoRequestOrder()
        {
            AutomaticRequestOrder = false;
        }

        public void Active()
        {
            IsActive = true;
        }

        public void Deactive()
        {
            IsActive = false;
        }


        public void SetReceivingPolicy(
            InspectionMode mode,
            int? deadlineHours,
            bool allowOneStep,
            long? holdZoneId)
        {
            if (ReceivingPolicy == null)
                ReceivingPolicy = new StoreReceivingPolicy(Id, mode, deadlineHours, allowOneStep, holdZoneId);
            else
                ReceivingPolicy.Update(mode, deadlineHours, allowOneStep, holdZoneId);
        }

        public void SetPlacementPolicy(
            PlacementPolicyMode mode,
            bool useAta = false,
            bool useKeyword = false,
            bool useStockType = false,
            bool useAircraftModel = false,
            bool usePartPhysical = false,
            bool useLocationKind = false,
            bool useEnvironmentalClass = false)
        {
            if (PlacementPolicy == null)
                PlacementPolicy = new StorePlacementPolicy(
                    Id, mode, useAta, useKeyword, useStockType,
                    useAircraftModel, usePartPhysical, useLocationKind, useEnvironmentalClass);
            else
                PlacementPolicy.Update(
                    mode, useAta, useKeyword, useStockType,
                    useAircraftModel, usePartPhysical, useLocationKind, useEnvironmentalClass);
        }

        /// <summary>هنگام ایجاد Store فراخوانی شود</summary>
        public void EnsureDefaultPolicies()
        {
            ReceivingPolicy ??= new StoreReceivingPolicy(Id);
            PlacementPolicy ??= new StorePlacementPolicy(Id);
        }

    }
}
