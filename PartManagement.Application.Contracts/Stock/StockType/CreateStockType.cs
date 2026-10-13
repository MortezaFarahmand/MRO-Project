using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace PartManagement.Application.Contracts.Stock.StockType
{
    public class CreateStockType
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Code { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public long OrganizationId { get; set; }

        public string ColorCode { get; set; }

        public string CreationDate { get; set; }
    }
}
