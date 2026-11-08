using PartManagement.Domain.StockAgg.StoreAgg.Enum;

namespace PartManagement.Domain.StockAgg.StoreAgg
{
    /// <summary>
    /// سیاست چیدمان انبار.
    /// پیش‌فرض: Mode = Off و همه معیارها غیرفعال.
    /// </summary>
    public class StorePlacementPolicy
    {
        public long StoreId { get; private set; }

        public PlacementPolicyMode Mode { get; private set; }

        /// <summary>چیدمان بر اساس ATA Chapter (BasicData.AtaChapterId)</summary>
        public bool UseAtaChapter { get; private set; }

        /// <summary>بر اساس Keyword قطعه (Pump, Switch, ...)</summary>
        public bool UseKeywordGroup { get; private set; }

        /// <summary>بر اساس StockType</summary>
        public bool UseStockType { get; private set; }

        /// <summary>بر اساس مدل/تایپ هواپیما</summary>
        public bool UseAircraftModel { get; private set; }

        /// <summary>بر اساس ابعاد/وزن/حجم قطعه</summary>
        public bool UsePartPhysical { get; private set; }

        /// <summary>بر اساس نوع لوکیشن (قفسه، رک، سردخانه، ...)</summary>
        public bool UseLocationKind { get; private set; }

        /// <summary>بر اساس کلاس محیطی (Ambient, ESD, Flammable, ...)</summary>
        public bool UseEnvironmentalClass { get; private set; }

        protected StorePlacementPolicy() { }

        /// <summary>ایجاد امن؛ چیدمان خاموش</summary>
        public StorePlacementPolicy(long storeId)
        {
            StoreId = storeId;
            Mode = PlacementPolicyMode.Off;
            UseAtaChapter = false;
            UseKeywordGroup = false;
            UseStockType = false;
            UseAircraftModel = false;
            UsePartPhysical = false;
            UseLocationKind = false;
            UseEnvironmentalClass = false;
        }

        public StorePlacementPolicy(
            long storeId,
            PlacementPolicyMode mode,
            bool useAta = false,
            bool useKeyword = false,
            bool useStockType = false,
            bool useAircraftModel = false,
            bool usePartPhysical = false,
            bool useLocationKind = false,
            bool useEnvironmentalClass = false)
        {
            StoreId = storeId;
            Mode = mode;
            UseAtaChapter = useAta;
            UseKeywordGroup = useKeyword;
            UseStockType = useStockType;
            UseAircraftModel = useAircraftModel;
            UsePartPhysical = usePartPhysical;
            UseLocationKind = useLocationKind;
            UseEnvironmentalClass = useEnvironmentalClass;
        }

        public void Update(
            PlacementPolicyMode mode,
            bool useAta = false,
            bool useKeyword = false,
            bool useStockType = false,
            bool useAircraftModel = false,
            bool usePartPhysical = false,
            bool useLocationKind = false,
            bool useEnvironmentalClass = false)
        {
            Mode = mode;
            UseAtaChapter = useAta;
            UseKeywordGroup = useKeyword;
            UseStockType = useStockType;
            UseAircraftModel = useAircraftModel;
            UsePartPhysical = usePartPhysical;
            UseLocationKind = useLocationKind;
            UseEnvironmentalClass = useEnvironmentalClass;
        }

        public bool HasAnyCriterion =>
            UseAtaChapter
            || UseKeywordGroup
            || UseStockType
            || UseAircraftModel
            || UsePartPhysical
            || UseLocationKind
            || UseEnvironmentalClass;

        public bool IsEnforced =>
            Mode != PlacementPolicyMode.Off && HasAnyCriterion;
    }
}