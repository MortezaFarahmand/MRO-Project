using PartManagement.Domain.StockAgg.StoreAgg.Enum;

namespace PartManagement.Domain.StockAgg.StoreAgg
{
    /// <summary>
    /// سیاست بازرسی ورود برای یک انبار.
    /// پیش‌فرض: بدون بازرسی (None) تا مدیر سیستم صریحاً فعال کند.
    /// </summary>
    public class StoreReceivingPolicy
    {
        public long StoreId { get; private set; }

        public InspectionMode InspectionMode { get; private set; }

        /// <summary>مهلت بازرسی به ساعت؛ null یعنی بدون مهلت زمانی</summary>
        public int? InspectionDeadlineHours { get; private set; }

        /// <summary>دریافت و بازرسی در یک مرحله برای کاربر مجاز</summary>
        public bool AllowOneStepReceiveAndInspect { get; private set; }

        /// <summary>Zone موقت Hold/Quarantine بعد از دریافت و قبل از تأیید</summary>
        public long? DefaultReceivingHoldZoneId { get; private set; }

        protected StoreReceivingPolicy() { }

        /// <summary>ایجاد امن با پیش‌فرض بدون بازرسی</summary>
        public StoreReceivingPolicy(long storeId)
        {
            StoreId = storeId;
            InspectionMode = InspectionMode.None;
            InspectionDeadlineHours = null;
            AllowOneStepReceiveAndInspect = false;
            DefaultReceivingHoldZoneId = null;
        }

        public StoreReceivingPolicy(
            long storeId,
            InspectionMode mode,
            int? deadlineHours,
            bool allowOneStep,
            long? holdZoneId)
        {
            StoreId = storeId;
            InspectionMode = mode;
            InspectionDeadlineHours = deadlineHours > 0 ? deadlineHours : null;
            AllowOneStepReceiveAndInspect = allowOneStep;
            DefaultReceivingHoldZoneId = holdZoneId;
        }

        public void Update(
            InspectionMode mode,
            int? deadlineHours,
            bool allowOneStep,
            long? holdZoneId)
        {
            InspectionMode = mode;
            InspectionDeadlineHours = deadlineHours > 0 ? deadlineHours : null;
            AllowOneStepReceiveAndInspect = allowOneStep;
            DefaultReceivingHoldZoneId = holdZoneId;
        }

        public bool RequiresInspection =>
            InspectionMode == InspectionMode.Mandatory
            || InspectionMode == InspectionMode.Warning
            || InspectionMode == InspectionMode.Optional;

        public bool IsMandatory => InspectionMode == InspectionMode.Mandatory;
    }
}