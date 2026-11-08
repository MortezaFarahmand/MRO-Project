using System.Collections.Generic;

namespace BasicDataManagement.Domain.ATAChapterAgg
{
    public static class PageBlockSeedData
    {
        public static List<MaintenancePageBlock> GetAll() => new()
    {
        MaintenancePageBlock.Create("D&O", "Description and Operation", "توضیحات و عملکرد", 1, 99, 1),
        MaintenancePageBlock.Create("TS",  "Troubleshooting",           "عیب‌یابی",         101, 199, 2),
        MaintenancePageBlock.Create("MP",  "Maintenance Practices",     "عملیات نگهداری",   201, 299, 3),
        MaintenancePageBlock.Create("SRV", "Servicing",                 "سرویس‌دهی",        301, 399, 4),
        MaintenancePageBlock.Create("R/I", "Removal/Installation",      "برداشت و نصب",     401, 499, 5),
        MaintenancePageBlock.Create("A/T", "Adjustment/Test",           "تنظیم و آزمون",    501, 599, 6),
        MaintenancePageBlock.Create("I/C", "Inspection/Check",          "بازرسی و کنترل",   601, 699, 7),
        MaintenancePageBlock.Create("C/P", "Cleaning/Painting",         "تمیزکاری و رنگ",   701, 799, 8),
        MaintenancePageBlock.Create("REP", "Repairs",                   "تعمیرات",          801, 899, 9),
    };
    }
}
