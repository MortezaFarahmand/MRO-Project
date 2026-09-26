using _0_Framework.Infrastructure;
using PartManagement.Application.Contracts.Stock.StockType;
using PartManagement.Domain.Stock.StockTypeAgg;
namespace PartManagement.Infrastructure.EFCore.Repository.Stock
{
    public class StockTypeRepository : RepositoryBase<long, StockType>, IStockTypeRepository
    {
        public readonly PartContext _context;
        public StockTypeRepository(PartContext context) : base(context)
        {
            _context = context;
        }


        
        public EditStockType Getdetails(long id)
        {
            return _context.StockTypes.Select(x => new EditStockType
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                ColorCode = x.ColorCode,
                CreationDate = x.CreationDate.ToString()
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<StockTypeViewModel> GetStockTypes()
        {
            return _context.StockTypes.Select(x => new StockTypeViewModel()
            {

                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                ColorCode = x.ColorCode,
                CreationDate = x.CreationDate.ToString()

            }).ToList();
        }

        public List<StockTypeViewModel> Search(StockTypeSearchModel searchModel)
        {
            var query = _context.StockTypes.Select(x => new StockTypeViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                ColorCode = x.ColorCode,
                CreationDate = x.CreationDate.ToString()

            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            return query.OrderByDescending(x => x.Id).ToList();
        }


    }
}
