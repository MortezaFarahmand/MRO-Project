using System.Collections.Generic;

namespace BasicDataManagement.Application.Contracts.ATAChapter.DTOs
{
    public class AtaSectionDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;      // "41"
        public string FullCode { get; set; } = null!;  // "32-41"
        public string TitleEn { get; set; } = null!;
        public string? TitleFa { get; set; }
        public List<AtaSubjectDto> Subjects { get; set; } = new();
    }
}
