using PartManagement.Domain.PartAgg.Enum;

namespace PartManagement.Application.Contracts.Part.Commands
{
    public class EditPartAlternatesForPartCommand
    {
        public long PrimaryPartId { get; set; }

        /// <summary>لیست نهایی Alternateها بعد از ویرایش</summary>
        public List<long> AlternatePartIds { get; set; } = new();

        public AlternateRelationType? RelationType { get; set; } = AlternateRelationType.TwoWay;
        public AlternateCategory? AlternateCategory { get; set; }
        public int? PreferenceLevel { get; set; } = 1;
        public bool? IsApproved { get; set; } = true;
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public string? Notes { get; set; }
    }
}
