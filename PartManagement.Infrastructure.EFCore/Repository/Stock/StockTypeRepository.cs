using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PartManagement.Application.Contracts.Stock.StockType;
using PartManagement.Domain.Stock.StockTypeAgg;
namespace PartManagement.Infrastructure.EFCore.Repository.Stock
{
    public class StockTypeRepository : RepositoryBase<long, StockType>, IStockTypeRepository
    {
        public readonly PartContext _partContext;
        public StockTypeRepository(PartContext partContext) : base(partContext)
        {
            _partContext = partContext;
        }


        
        public EditStockType Getdetails(long id)
        {
            return _partContext.StockTypes.Select(x => new EditStockType
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
            //var store = _context.Stores.Select(x => new { x.Id, x.OrganizationId }).ToList();
            var query = _partContext.StockTypes.Select(x => new StockTypeViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                //Store = x.Store.Name,
                ColorCode = x.ColorCode,
                CreationDate = x.CreationDate.ToString()
            });

            //if (organizationId != 0)
            //    query = query.Where(x => x.OrganizationId == organizationId);

            return query.OrderByDescending(x => x.Id).ToList();
        }

        public List<StockTypeViewModel> Search(StockTypeSearchModel searchModel)
        {
            var query = _partContext.StockTypes.Select(x => new StockTypeViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                //Store = x.Store.Name,
                ColorCode = x.ColorCode,
                CreationDate = x.CreationDate.ToString()

            });

            //if (organizationId != 0)
            //    query = query.Where(x => x.OrganizationId == organizationId);

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            return query.OrderByDescending(x => x.Id).ToList();
        }

    }
}
