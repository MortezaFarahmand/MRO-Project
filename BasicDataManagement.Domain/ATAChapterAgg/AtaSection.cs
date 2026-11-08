using System.Collections.Generic;
namespace BasicDataManagement.Domain.ATAChapterAgg
{
    public class AtaSection
    {
        public int Id { get; private set; }
        public int AtaChapterId { get; private set; }
        public AtaChapter Chapter { get; private set; }

        public string Code { get; private set; }          // "41"  →  کامل می‌شود 32-41
        public string FullCode { get; private set; }      // "32-41"
        public string TitleEn { get; private set; }
        public string? TitleFa { get; private set; }

        public ICollection<AtaSubject> Subjects { get; private set; } = new List<AtaSubject>();
    }
}