namespace PartManagement.Application.Contracts.Stock.Store
{
    public class StoreSearchModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long StockTypeId { get; set; }
        public long? OrganizationId { get; set; }
        public bool IsAdmin { get; set; }
    }
}
