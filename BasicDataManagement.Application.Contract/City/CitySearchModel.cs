namespace BasicDataManagement.Application.Contracts.City
{
    public class CitySearchModel
    {
        public string Name { get; set; }
        public string DialCode { get; set; }
        public long ProvinceId { get; set; }
        public long CountryId { get; set; }
    }
}
