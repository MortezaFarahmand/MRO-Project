using _0_Framework.Application;
using BasicDataManagement.Application.Contracts.Airport;
using BasicDataManagement.Application.Contracts.City;
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
        public List<OrganizationViewModel> Organizations { get; set; } = new();
        public long CityId { get; set; }
        public long StationId { get; set; }

        [Range(1, 100000, ErrorMessage = ValidationMessages.IsRequired)]
        public long StockTypeId { get; set; }

        public List<StockTypeViewModel> StockTypes { get; set; } = new();

        public long ParentId { get; set; }
        public long ProviderId { get; set; }
        public bool NotToSearchBySpecificStore { get; set; }
        public bool AutomaticRequestOrder { get; set; }
        public List<long> StockTypeIds { get; set; } = new();
        public bool IsAdmin { get; set; }
        public List<CityViewModel> Cities { get; set; } = new();
        public List<AirportViewModel> Stations { get; set; } = new();
        public List<StoreViewModel> Stores { get; set; } = new();
    }
}
