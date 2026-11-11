
namespace PartManagement.Application.Contracts.Stock.DTOs
{
    public class CitySimpleDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ProvinceId { get; set; }
        public string Province { get; set; }
        public string? ProvinceName { get; set; }
        public string Display { get; set; }
    }
}
