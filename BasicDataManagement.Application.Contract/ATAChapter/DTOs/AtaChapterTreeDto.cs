using System.Collections.Generic;

namespace BasicDataManagement.Application.Contracts.ATAChapter.DTOs
{
    public class AtaChapterTreeDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string TitleEn { get; set; } = null!;
        public string? TitleFa { get; set; }
        public string Group { get; set; } = null!;
        public List<AtaSectionDto> Sections { get; set; } = new();
    }
}
