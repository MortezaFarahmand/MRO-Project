//using BasicDataManagement.Domain.ATAChapterAgg.Enum;
using BasicDataManagement.Application.Contracts.ATAChapter;
using System.Collections.Generic;

namespace BasicDataManagement.Domain.ATAChapterAgg
{
    public static class AtaChapterSeedData
    {
        public static List<AtaChapter> GetAll()
        {
            return new List<AtaChapter>
        {
            // Aircraft General
            AtaChapter.Create("05", "Time Limits/Maintenance Checks", AtaGroup.GeneralAircraftInfo, "محدودیت‌های زمانی / بازرسی‌های نگهداری", order: 5),
            AtaChapter.Create("06", "Dimensions and Areas", AtaGroup.GeneralAircraftInfo, "ابعاد و نواحی", order: 6),
            AtaChapter.Create("07", "Lifting and Shoring", AtaGroup.GeneralAircraftInfo, "بالابری و شمع‌گذاری", order: 7),
            AtaChapter.Create("08", "Leveling and Weighing", AtaGroup.GeneralAircraftInfo, "تراز و وزن‌کشی", order: 8),
            AtaChapter.Create("09", "Towing and Taxiing", AtaGroup.GeneralAircraftInfo, "کشیدن و تاکسی", order: 9),
            AtaChapter.Create("10", "Parking and Mooring", AtaGroup.GeneralAircraftInfo, "پارک و مهار", order: 10),
            AtaChapter.Create("11", "Placards and Markings", AtaGroup.GeneralAircraftInfo, "پلاکاردها و علامت‌گذاری‌ها", order: 11),
            AtaChapter.Create("12", "Servicing", AtaGroup.GeneralAircraftInfo, "سرویس‌دهی", order: 12),

            // Airframe Systems
            AtaChapter.Create("20", "Standard Practices - Airframe", AtaGroup.AirframeSystems, "روش‌های استاندارد - بدنه", order: 20),
            AtaChapter.Create("21", "Air Conditioning", AtaGroup.AirframeSystems, "تهویه مطبوع و فشار", order: 21),
            AtaChapter.Create("22", "Auto Flight", AtaGroup.AirframeSystems, "پرواز خودکار", order: 22),
            AtaChapter.Create("23", "Communications", AtaGroup.AirframeSystems, "ارتباطات", order: 23),
            AtaChapter.Create("24", "Electrical Power", AtaGroup.AirframeSystems, "قدرت الکتریکی", order: 24),
            AtaChapter.Create("25", "Equipment/Furnishings", AtaGroup.AirframeSystems, "تجهیزات و مبلمان", order: 25),
            AtaChapter.Create("26", "Fire Protection", AtaGroup.AirframeSystems, "حفاظت در برابر آتش", order: 26),
            AtaChapter.Create("27", "Flight Controls", AtaGroup.AirframeSystems, "کنترل‌های پرواز", order: 27),
            AtaChapter.Create("28", "Fuel", AtaGroup.AirframeSystems, "سوخت", order: 28),
            AtaChapter.Create("29", "Hydraulic Power", AtaGroup.AirframeSystems, "قدرت هیدرولیک", order: 29),
            AtaChapter.Create("30", "Ice and Rain Protection", AtaGroup.AirframeSystems, "حفاظت در برابر یخ و باران", order: 30),
            AtaChapter.Create("31", "Indicating/Recording Systems", AtaGroup.AirframeSystems, "سیستم‌های نشان‌دهنده/ثبت", order: 31),
            AtaChapter.Create("32", "Landing Gear", AtaGroup.AirframeSystems, "چرخ و ارابه فرود", order: 32),
            AtaChapter.Create("33", "Lights", AtaGroup.AirframeSystems, "چراغ‌ها", order: 33),
            AtaChapter.Create("34", "Navigation", AtaGroup.AirframeSystems, "ناوبری", order: 34),
            AtaChapter.Create("35", "Oxygen", AtaGroup.AirframeSystems, "اکسیژن", order: 35),
            AtaChapter.Create("36", "Pneumatic", AtaGroup.AirframeSystems, "پنوماتیک", order: 36),
            AtaChapter.Create("38", "Water/Waste", AtaGroup.AirframeSystems, "آب و فاضلاب", order: 38),
            AtaChapter.Create("44", "Cabin Systems", AtaGroup.AirframeSystems, "سیستم‌های کابین", order: 44),
            AtaChapter.Create("45", "Central Maintenance System", AtaGroup.AirframeSystems, "سیستم نگهداری مرکزی", order: 45),
            AtaChapter.Create("46", "Information Systems", AtaGroup.AirframeSystems, "سیستم‌های اطلاعات", order: 46),
            AtaChapter.Create("47", "Inert Gas System", AtaGroup.AirframeSystems, "سیستم گاز خنثی", order: 47),
            AtaChapter.Create("49", "Airborne Auxiliary Power", AtaGroup.AirframeSystems, "نیروی کمکی هوابرد (APU)", order: 49),

            // Structures
            AtaChapter.Create("51", "Structures - General", AtaGroup.Structures, "سازه‌ها - عمومی", order: 51),
            AtaChapter.Create("52", "Doors", AtaGroup.Structures, "درها", order: 52),
            AtaChapter.Create("53", "Fuselage", AtaGroup.Structures, "بدنه", order: 53),
            AtaChapter.Create("54", "Nacelles/Pylons", AtaGroup.Structures, "ناسل/پیلون", order: 54),
            AtaChapter.Create("55", "Stabilizers", AtaGroup.Structures, "پایدارکننده‌ها", order: 55),
            AtaChapter.Create("56", "Windows", AtaGroup.Structures, "پنجره‌ها", order: 56),
            AtaChapter.Create("57", "Wings", AtaGroup.Structures, "بال‌ها", order: 57),

            // Power Plant
            AtaChapter.Create("70", "Standard Practices - Engine", AtaGroup.PowerPlant, "روش‌های استاندارد - موتور", order: 70),
            AtaChapter.Create("71", "Power Plant", AtaGroup.PowerPlant, "نیروگاه", order: 71),
            AtaChapter.Create("72", "Engine", AtaGroup.PowerPlant, "موتور", order: 72),
            AtaChapter.Create("73", "Engine Fuel and Control", AtaGroup.PowerPlant, "سوخت و کنترل موتور", order: 73),
            AtaChapter.Create("74", "Ignition", AtaGroup.PowerPlant, "احتراق", order: 74),
            AtaChapter.Create("75", "Air", AtaGroup.PowerPlant, "هوا (Bleed Air)", order: 75),
            AtaChapter.Create("76", "Engine Controls", AtaGroup.PowerPlant, "کنترل‌های موتور", order: 76),
            AtaChapter.Create("77", "Engine Indicating", AtaGroup.PowerPlant, "نشان‌دهنده‌های موتور", order: 77),
            AtaChapter.Create("78", "Exhaust", AtaGroup.PowerPlant, "خروجی (Thrust Reverser)", order: 78),
            AtaChapter.Create("79", "Oil", AtaGroup.PowerPlant, "روغن", order: 79),
            AtaChapter.Create("80", "Starting", AtaGroup.PowerPlant, "راه‌اندازی", order: 80),
        };
        }
    }
}