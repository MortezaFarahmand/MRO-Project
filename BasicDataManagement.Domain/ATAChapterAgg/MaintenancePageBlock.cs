
namespace BasicDataManagement.Domain.ATAChapterAgg
{
    public class MaintenancePageBlock
    {
        public int Id { get; private set; }
        public string Code { get; private set; }          // "R/I"
        public string TitleEn { get; private set; }       // "Removal/Installation"
        public string TitleFa { get; private set; }
        public int StartPage { get; private set; }        // 401
        public int EndPage { get; private set; }          // 499
        public int DisplayOrder { get; private set; }

        protected MaintenancePageBlock() { }   // برای EF Core

        public static MaintenancePageBlock Create(
        string code, string titleEn, string? titleFa,
        int startPage, int endPage, int displayOrder)
        {
            return new MaintenancePageBlock
            {
                Code = code,
                TitleEn = titleEn,
                TitleFa = titleFa,
                StartPage = startPage,
                EndPage = endPage,
                DisplayOrder = displayOrder
            };
        }

        public MaintenancePageBlock(string code, string titleEn, string? titleFa, int startPage, int endPage)
        {
            Code = code;
            TitleEn = titleEn;
            TitleFa = titleFa;
            StartPage = startPage;
            EndPage = endPage;
        }
    }
}