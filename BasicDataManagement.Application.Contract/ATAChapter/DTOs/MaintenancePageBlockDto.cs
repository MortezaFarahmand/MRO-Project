namespace BasicDataManagement.Application.Contracts.ATAChapter.DTOs
{
    public class MaintenancePageBlockDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string TitleEn { get; set; } = null!;
        public string? TitleFa { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int DisplayOrder { get; set; }
    }
}
