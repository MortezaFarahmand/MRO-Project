// EnvironmentalClass  (شرایط نگهداری)
namespace PartManagement.Domain.StockAgg.StoreAgg.Enum
{
    public enum EnvironmentalClass
    {
        Ambient = 0,
        ControlledHumidity = 1,
        Refrigerated = 2,
        Freezer = 3,
        ESD = 4,          // قطعات الکترونیکی حساس
        Flammable = 5,
        Hazardous = 6,
        Custom = 99
    }
}