namespace PartManagement.Domain.InventoryAgg.InventoryItemAgg.Enum
{
    public enum InventoryStatus
    {
        InStock = 1,          // موجود در انبار
        Allocated = 2,        // رزرو شده برای Work Order
        Installed = 3,        // نصب‌شده روی هواپیما
        OutForRepair = 4,     // ارسال‌شده برای تعمیر
        Quarantined = 5,      // قرنطینه
        Scrapped = 6,         // اسقاط
        InTransit = 7,        // در حال انتقال
        ReservedBySupervisor = 8,
        Unknown = 9           // نا معلوم - Missed
        
    }
}
