// Edit تک‌رکوردی (اختیاری – برای ویرایش یک رابطه)
using PartManagement.Domain.PartAgg.Enum;

namespace PartManagement.Application.Contracts.Part.Commands
{
    public class EditPartAlternateCommand
    {
        public long Id { get; set; }
        public AlternateRelationType? RelationType { get; set; }
        public AlternateCategory? AlternateCategory { get; set; }
        public int? PreferenceLevel { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public string? Notes { get; set; }
    }

}