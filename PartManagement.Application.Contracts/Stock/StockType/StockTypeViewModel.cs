namespace PartManagement.Application.Contracts.Stock.StockType
{
    public class StockTypeViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ColorCode { get; set; }
        public string Store { get; set; }
        public long StoreId { get; set; }
        public string CreationDate { get; set; }
        public long OrganizationId { get; set; }
    }
}
