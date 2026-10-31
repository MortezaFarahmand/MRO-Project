using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartManagement.Domain.InventoryAgg.StockMovementAgg.Enums
{
    public enum StockMovementType
    {
        Receipt = 1,              // ورود به انبار (از خرید، تعمیر، برگشت و ...)
        Issue = 2,                // خروج از انبار (برای نصب، فروش، تعمیر و ...)
        Transfer = 3,             // انتقال بین انبارها
        Adjustment = 4,           // تعدیل موجودی (انبارگردانی)
        ReturnToStock = 5,        // برگشت به انبار
        Scrap = 6,                // اسقاط
        Quarantine = 7            // انتقال به قرنطینه
    }
}
