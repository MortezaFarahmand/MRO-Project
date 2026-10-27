using PartManagement.Application.Contracts.Stock.StockType;

namespace PartManagement.Application.Contracts.Stock.Store
{
    public class StoreViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Organization { get; set; }
        public string City { get; set; }
        public string Station { get; set; }
        public string StockType { get; set; }
        public string Parent { get; set; }
        public string Provider { get; set; }
        public long StockTypeId { get; set; }
        public long? OrganizationId { get; set; }
        public string CreationDate { get; set; }

        public bool IsActive { get; set; }
        public List<StockTypeViewModel> StockTypes { get; set; } = new();
        public long CityId { get; set; }
        public long StationId { get; set; }
        public long ParentId { get; set; }
    }
}
