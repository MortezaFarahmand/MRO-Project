//using BasicDataManagement.Domain.ATAChapterAgg.Enum;
using BasicDataManagement.Application.Contracts.ATAChapter;
using System.Collections.Generic;

namespace BasicDataManagement.Domain.ATAChapterAgg
{
    public class AtaChapter
    {
        public int Id { get; private set; }
        public string Code { get; private set; }          // "32"
        public string TitleEn { get; private set; }
        public string TitleFa { get; private set; }        // اختیاری برای فارسی
        public AtaGroup Group { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; } = true;
        public int DisplayOrder { get; private set; }

        public ICollection<AtaSection> Sections { get; private set; } = new List<AtaSection>();

        // سازنده خصوصی + متد کارخانه‌ای برای EF Core
        private AtaChapter() { }

        public static AtaChapter Create(string code, string titleEn, AtaGroup group,
                                        string? titleFa = null, string? description = null, int order = 0)
        {
            return new AtaChapter
            {
                Code = code,
                TitleEn = titleEn,
                TitleFa = titleFa ?? titleEn,
                Group = group,
                Description = description,
                DisplayOrder = order
            };
        }
    }
}