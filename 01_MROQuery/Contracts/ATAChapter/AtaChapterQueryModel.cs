using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_MROQuery.Contracts.ATAChapter
{
    public class AtaChapterQueryModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string TitleEn { get; set; }
        public string TitleFa { get; set; }
        public string Group { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }

        public string Display =>
            string.IsNullOrWhiteSpace(TitleEn)
                ? Code
                : $"{Code} - {TitleEn}";
    }
}
