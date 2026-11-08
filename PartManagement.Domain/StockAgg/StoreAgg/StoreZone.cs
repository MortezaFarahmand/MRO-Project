// PartManagement.Domain/StockAgg/StoreAgg/StoreZone.cs
using _0_Framework.Domain;
using PartManagement.Domain.StockAgg.StoreAgg.Enum;

namespace PartManagement.Domain.StockAgg.StoreAgg
{
    public class StoreZone : EntityBase
    {
        public long StoreId { get; private set; }

        public string Code { get; private set; }
        public string Name { get; private set; }

        public ZoneType ZoneType { get; private set; }

        /// <summary>مرجع به BasicData – ATA Chapter</summary>
        public int? AtaChapterId { get; private set; }

        /// <summary>گروه کلیدواژه (Pump, Switch, ...)</summary>
        public string KeywordGroup { get; private set; }

        /// <summary>مدل/تایپ هواپیما در صورت نیاز به جداسازی چیدمان</summary>
        public long? AircraftModelId { get; private set; }

        public LocationKind? LocationKind { get; private set; }
        public EnvironmentalClass? EnvironmentalClass { get; private set; }

        public decimal? MaxWeightKg { get; private set; }
        public decimal? MaxVolumeM3 { get; private set; }

        public bool IsActive { get; private set; }

        protected StoreZone() { }

        public StoreZone(
            long storeId,
            string code,
            string name,
            ZoneType zoneType,
            int? ataChapterId = null,
            string keywordGroup = null,
            long? aircraftModelId = null,
            LocationKind? locationKind = null,
            EnvironmentalClass? environmentalClass = null,
            decimal? maxWeightKg = null,
            decimal? maxVolumeM3 = null)
        {
            StoreId = storeId;
            Code = NormalizeRequired(code, nameof(code));
            Name = NormalizeRequired(name, nameof(name));
            ZoneType = zoneType;
            AtaChapterId = ataChapterId;
            KeywordGroup = NormalizeOptional(keywordGroup);
            AircraftModelId = aircraftModelId;
            LocationKind = locationKind;
            EnvironmentalClass = environmentalClass;
            MaxWeightKg = maxWeightKg;
            MaxVolumeM3 = maxVolumeM3;
            IsActive = true;
        }

        public void Edit(
            string name,
            ZoneType zoneType,
            int? ataChapterId,
            string keywordGroup,
            long? aircraftModelId,
            LocationKind? locationKind,
            EnvironmentalClass? environmentalClass,
            decimal? maxWeightKg,
            decimal? maxVolumeM3)
        {
            Name = NormalizeRequired(name, nameof(name));
            ZoneType = zoneType;
            AtaChapterId = ataChapterId;
            KeywordGroup = NormalizeOptional(keywordGroup);
            AircraftModelId = aircraftModelId;
            LocationKind = locationKind;
            EnvironmentalClass = environmentalClass;
            MaxWeightKg = maxWeightKg;
            MaxVolumeM3 = maxVolumeM3;
        }

        public void Activate() => IsActive = true;

        public void Deactivate() => IsActive = false;

        /// <summary>
        /// آیا این Zone برای Hold بعد از دریافت مناسب است؟
        /// </summary>
        public bool IsReceivingHold =>
            ZoneType == ZoneType.ReceivingHold || ZoneType == ZoneType.Quarantine;

        private static string NormalizeRequired(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new System.ArgumentException("مقدار الزامی است.", paramName);
            return value.Trim();
        }

        private static string NormalizeOptional(string value) =>
            string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}