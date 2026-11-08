namespace BasicDataManagement.Application.Contracts.ATAChapter.DTOs
{
    public class AtaSubjectDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;      // "01"
        public string FullCode { get; set; } = null!;  // "32-41-01"
        public string TitleEn { get; set; } = null!;
        public string? TitleFa { get; set; }
        public string? WdmChapterSection { get; set; }
        public string? IpcChapterSection { get; set; }
    }
}