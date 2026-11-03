using PartManagement.Domain.PartAgg.Enum;

namespace PartManagement.Application.Contracts.Part.Queries
{
    public class PartAlternateSearchModel
    {
        public long? PrimaryPartId { get; set; }
        public long? AlternatePartId { get; set; }
        public string PartNumber { get; set; } // جستجو روی هر دو طرف
        public AlternateCategory? AlternateCategory { get; set; }
        public bool? IsApproved { get; set; }
    }
}
