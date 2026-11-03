using PartManagement.Domain.PartAgg.Enum;

namespace PartManagement.Application.Contracts.Part.Commands
{
    public class CreatePartAlternateCommand
    {
        public long PrimaryPartId { get; set; }
        public long AlternatePartId { get; set; }
        public AlternateRelationType? RelationType { get; set; } 

        public AlternateCategory? AlternateCategory { get; set; }

        public int? PreferenceLevel { get; set; } = 1;
        public bool? IsApproved { get; set; } = true;
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public string? Notes { get; set; }
        /// <summary>چند قطعه جایگزین هم‌زمان</summary>
        public List<long> AlternatePartIds { get; set; } = new();
    }

}