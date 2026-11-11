
namespace PartManagement.Application.Contracts.Stock.DTOs
{
    public class StoreSimpleDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string DefaultStockType { get; set; }

        // برای نمایش در دراپ‌داون
        public string Display => string.IsNullOrWhiteSpace(DefaultStockType)
            ? Name
            : $"{Name} ({DefaultStockType})";
    }
}
