using PartManagement.Domain.PartAgg.Enum;

namespace PartManagement.Application.Contracts.Part.Commands
{
    public class CreatePartCommand
    {
        public string PartNumber { get; set; }
        public string Description { get; set; }
        public string Keyword { get; set; }
        public string Specification { get; set; }
        public PartControlType ControlType { get; set; }
        public bool IsGlobal { get; set; }
        public long? OwnerOrganizationId { get; set; }

        public long? PartTypeId { get; set; }
        public long? EquipmentTypeId { get; set; }
        public long? ClassificationId { get; set; }
        public long? PurchaseUnitId { get; set; }
        public long? StorageUnitId { get; set; }
        public long? StockPolicyId { get; set; }

        public string Note { get; set; }
    }
}
