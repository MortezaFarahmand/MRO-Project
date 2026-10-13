using _0_Framework.Domain;
using PartManagement.Domain.Stock.StockTypeAgg;

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
        public long OrganizationId { get; private set; }
        public long CityId { get; private set; }
        public long StationId { get; private set; }
        public long StockTypeId { get; private set; }
        //public List<StoreRoom> StoreRooms { get; private set; }
        public StockType StockType { get; private set; }
        public long ParentId { get; private set; }
        public  long ProviderId { get; private set; }
        public bool NotToSearchBySpecificStore { get; private set; }
        public bool AutomaticRequestOrder { get; private set; }
        public bool IsActive { get; private set; }

        public Store()
        {
            //StoreRooms = new List<StoreRoom>();
        }

        public Store(string name, string nameFa, string code, string colorCode, 
            string description, long organizationId, long cityId, long stationId, long stockTypeId, 
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
            StockTypeId = stockTypeId;
            ParentId = parentId;
            ProviderId = providerId;
            DefaultValueOfStockMovement = defaultValueOfStockMovement;
            IsActive = true;
            NotToSearchBySpecificStore = false;
            AutomaticRequestOrder = false;

        }

        public void Edit(string name, string nameFa, string code, string colorCode,
            string description, long organizationId, long cityId, long stationId, long stockTypeId,
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
            StockTypeId = stockTypeId;
            ParentId = parentId;
            ProviderId = providerId;
            DefaultValueOfStockMovement = defaultValueOfStockMovement;
        }

        //public void AddRoomToStore(StockType room)
        //{
        //    StockRooms.Add(room);
        //}
        //public void RemoveRoomFromStore(StockType room)
        //{
        //    StockRooms.Remove(room);
        //}

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


    }
}
