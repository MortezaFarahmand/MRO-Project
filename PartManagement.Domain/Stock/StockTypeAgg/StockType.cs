using _0_Framework.Domain;
namespace PartManagement.Domain.Stock.StockTypeAgg
{
    public class StockType : EntityBase
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string ColorCode { get; private set; }

        public StockType() 
        {

        }
         

        public StockType(string name, string code, string colorCode) 
        { 
            Name = name;
            Code = code;
            ColorCode = colorCode;
        }

        public void Edit(string name, string code, string colorCode)
        {
            Code = code;
            Name = name;
            ColorCode = colorCode;
        }
    }

    
}
