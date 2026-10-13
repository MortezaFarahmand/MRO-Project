using _0_Framework.Domain;
using PartManagement.Domain.Stock.StoreAgg;
namespace PartManagement.Domain.Stock.StockTypeAgg
{
    public class StockType : EntityBase
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string ColorCode { get; private set; }
        public long OrganizationId { get; private set; }
        public List<Store> Stores { get; private set; }

        public StockType() 
        {
            Stores = new List<Store>();
        }
         

        public StockType(string name, string code, string colorCode, long organizationId) 
        { 
            Name = name;
            Code = code;
            ColorCode = colorCode;
            OrganizationId = organizationId;
        }

        public void Edit(string name, string code, string colorCode, long organizationId)
        {
            Code = code;
            Name = name;
            ColorCode = colorCode;
            OrganizationId = organizationId;
        }
    }

    
}
