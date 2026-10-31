using PartManagement.Application.Contracts.Part.DTOs;

namespace PartManagement.Application.Contracts.Part.DTOs
{
    public class PartDto
    {
        public long Id { get; set; }
        public string PartNumber { get; set; }
        public string Revision { get; set; }
        public string Description { get; set; }
        public string Keyword { get; set; }
        public string Specification { get; set; }
        public string ControlType { get; set; }          // یا خود Enum
        public bool IsGlobal { get; set; }
        public long? OwnerOrganizationId { get; set; }
        public string Note { get; set; }

        public long? PartTypeId { get; set; }
        public long? EquipmentTypeId { get; set; }
        public long? ClassificationId { get; set; }
        public long? PurchaseUnitId { get; set; }
        public long? StorageUnitId { get; set; }
        public long? StockPolicyId { get; set; }

        public decimal? FloatQuantity { get; set; }
        public decimal? AveragePrice { get; set; }

        // در صورت نیاز می‌توانید لیست‌ها را هم اضافه کنید
        // public List<PartManufacturerDto> Manufacturers { get; set; }
        // public List<PartOrganizationDto> Organizations { get; set; }
    }
}
