using BasicDataManagement.Domain.ProvinceAgg;

namespace _01_MROQuery.Contracts.City
{
    public class CityQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ProvinceId { get; private set; }
        public Province Province { get; private set; }
        public string? ProvinceName { get; internal set; }
        public string Display { get; internal set; }
    }
}
