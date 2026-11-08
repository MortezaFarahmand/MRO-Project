namespace BasicDataManagement.Application.Contracts.ATAChapter.DTOs
{
    public class AtaChapterDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string TitleEn { get; set; } = null!;
        public string? TitleFa { get; set; }
        public string Group { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
    }
}