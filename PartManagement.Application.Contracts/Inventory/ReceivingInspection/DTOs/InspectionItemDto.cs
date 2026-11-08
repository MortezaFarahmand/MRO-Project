
namespace PartManagement.Application.Contracts.Inventory.ReceivingInspection.DTOs
{
    public class InspectionItemDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string HelpText { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public bool IsMandatory { get; set; }
        public bool AllowNotApplicable { get; set; }
        public string ApplicableKeyword { get; set; }
        public long? StockTypeId { get; set; }
    }
}
