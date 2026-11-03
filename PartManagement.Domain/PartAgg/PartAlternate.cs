using System;
using _0_Framework.Domain;
using PartManagement.Domain.PartAgg.Enum;

namespace PartManagement.Domain.PartAgg
{
    public class PartAlternate : EntityBase
    {
        public long PrimaryPartId { get; private set; }
        public long AlternatePartId { get; private set; }

        /// <summary>TwoWay | OneWay</summary>
        public AlternateRelationType? RelationType { get; private set; } = AlternateRelationType.TwoWay;

        /// <summary>OEM | PMA | SuperSession | DashHigher | Equivalent</summary>
        public AlternateCategory? AlternateCategory { get; private set; }

        /// <summary>عدد کوچکتر = اولویت بالاتر (1 = Preferred)</summary>
        public int? PreferenceLevel { get; private set; }

        public bool? IsApproved { get; private set; }
        public DateTime? EffectiveFrom { get; private set; }
        public DateTime? EffectiveTo { get; private set; }
        public string? Notes { get; private set; }
        
        

        public Part PrimaryPart { get; private set; }
        public Part AlternatePart { get; private set; }

        private PartAlternate() { }

        public PartAlternate(
            long primaryPartId,
            long alternatePartId,
            AlternateRelationType? relationType,
            AlternateCategory? alternateCategory,
            int? preferenceLevel,
            bool? isApproved,
            DateTime? effectiveFrom,
            DateTime? effectiveTo,
            string? notes)
        {
            if (primaryPartId == alternatePartId)
                throw new InvalidOperationException("Primary and Alternate part cannot be the same.");

            PrimaryPartId = primaryPartId;
            AlternatePartId = alternatePartId;
            // سازنده PartAlternate — اگر relationType خالی بود:
            RelationType = relationType == null
                ? AlternateRelationType.TwoWay
                : relationType;
            AlternateCategory = alternateCategory;
            PreferenceLevel = preferenceLevel <= 0 ? 1 : preferenceLevel;
            IsApproved = isApproved;
            EffectiveFrom = effectiveFrom;
            EffectiveTo = effectiveTo;
            Notes = notes;
        }

        public void Edit(
            AlternateRelationType? relationType,
            AlternateCategory? alternateCategory,
            int? preferenceLevel,
            bool? isApproved,
            DateTime? effectiveFrom,
            DateTime? effectiveTo,
            string? notes)
        {
            RelationType = relationType == null ? RelationType : relationType;
            AlternateCategory = alternateCategory;
            PreferenceLevel = preferenceLevel <= 0 ? PreferenceLevel : preferenceLevel;
            IsApproved = isApproved;
            EffectiveFrom = effectiveFrom;
            EffectiveTo = effectiveTo;
            Notes = notes;
        }

        public void Approve() => IsApproved = true;
        public void Unapprove() => IsApproved = false;

        public bool IsCurrentlyEffective(DateTime? onDate = null)
        {
            var d = onDate ?? DateTime.UtcNow;
            if (EffectiveFrom.HasValue && d < EffectiveFrom.Value) return false;
            if (EffectiveTo.HasValue && d > EffectiveTo.Value) return false;
            return true;
        }
    }

    
}