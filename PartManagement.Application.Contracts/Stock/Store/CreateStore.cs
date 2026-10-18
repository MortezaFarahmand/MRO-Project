using _0_Framework.Application;
using OrganizationManagement.Application.Contracts.Organization;
using PartManagement.Application.Contracts.Stock.StockType;
using System.ComponentModel.DataAnnotations;

namespace PartManagement.Application.Contracts.Stock.Store
{
    public class CreateStore
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get; set; }

        public string NameFa { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Code { get; set; }

        public string ColorCode { get; set; }
        public string Description { get; set; }
        public string DefaultValueOfStockMovement { get; set; }
        public long? OrganizationId { get; set; }
        public List<OrganizationViewModel> Organizations { get; set; }
        public long CityId { get; set; }
        public long StationId { get; set; }

        [Range(1, 100000, ErrorMessage = ValidationMessages.IsRequired)]
        public long StockTypeId { get; set; }

        public List<StockTypeViewModel> StockTypes { get; set; }

        public long ParentId { get; set; }
        public long ProviderId { get; set; }
        public bool NotToSearchBySpecificStore { get; set; }
        public bool AutomaticRequestOrder { get; set; }
        public List<long> StockTypeIds { get; set; } = new();

        //public class AddStockType
        //{
        //    public long StoreId { get; set; }
        //}
    }
}
