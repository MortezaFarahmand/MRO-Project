namespace PartManagement.Application.Contracts.Stock.StockType
{
    public class StockTypeSearchModel
    {
        public string? Name { get; set; }
        public long? StoreId { get; set; }
        public long? OrganizationId { get; set; }
        public bool IsAdmin { get; set; }

        // مدل سرچ نباید Required داشته باشه، چون قراره خالی هم سرچ بشه (یعنی نمایش همه). ?
    }
}
