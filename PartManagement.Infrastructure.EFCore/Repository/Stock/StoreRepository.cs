using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using OrganizationManagement.Domain.OrganizationAgg;
using OrganizationManagement.Infrastructure.EFCore;
using PartManagement.Application.Contracts.Stock.StockType;
using PartManagement.Application.Contracts.Stock.Store;
using PartManagement.Domain.Stock.StoreAgg;

namespace PartManagement.Infrastructure.EFCore.Repository.Stock
{
    public class StoreRepository : RepositoryBase<long, Store>, IStoreRepository
    {
        private readonly OrganizationContext _organizationContext;
        public readonly PartContext _partContext;
        public StoreRepository(PartContext partContext, OrganizationContext organizationContext) : base(partContext)
        {
            _partContext = partContext;
            _organizationContext = organizationContext;
        }


        public EditStore GetDetails(long id)
        {
            var store = _partContext.Stores
               .Include(s => s.StoreStockTypes)
                   .ThenInclude(sst => sst.StockType)
               .FirstOrDefault(x => x.Id == id);

            return _partContext.Stores.Select(x => new EditStore
            {
                Id = x.Id,
                Name = x.Name,
                NameFa = x.NameFa,
                Code = x.Code,
                ColorCode = x.ColorCode,
                Description = x.Description,
                DefaultValueOfStockMovement = x.DefaultValueOfStockMovement,
                OrganizationId = x.OrganizationId,
                CityId = x.CityId,
                StationId = x.StationId,
                StockTypeId = x.DefaultStockTypeId,
                ParentId = x.ParentId,
                ProviderId = x.ProviderId,
                StockTypeIds = x.StoreStockTypes.Select(sst => sst.StockTypeId).ToList()

            }).FirstOrDefault(x => x.Id == id);
        }


        public List<StoreViewModel> Search(StoreSearchModel searchModel)
        {
            searchModel ??= new StoreSearchModel();
            var query = _partContext.Stores.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name == searchModel.Name);

            if (!string.IsNullOrWhiteSpace(searchModel.Code))
                query = query.Where(x => x.Code == searchModel.Code);

            if (searchModel.StockTypeId != 0)
                query = query.Where(x => x.DefaultStockTypeId == searchModel.StockTypeId);

            // در این شرط در صورتی که آی دی سازمان اکانت وارد شده صفر باشد دسترسی به تمام استورهایی که آیدی آنها غیر صفر است دارد و
            // در غیر این صورت یعنی زمانی که آیدی سازمان اکانت وارد شده صفر نباشد فقط استورهایی را میتواند مشاهده کند که 
            // آیدی سازمان آن با آیدی سازمان انتخاب شده از سلکتور لیست استورها یکی باشد.
            //if (organizationId != 0)
            //    query = query.Where(x => x.OrganizationId == organizationId);

            //if (organizationId == 0 && searchModel.OrganizationId != 0)
            //    query = query.Where(x => x.OrganizationId == searchModel.OrganizationId);
            if (searchModel.OrganizationId.HasValue && searchModel.OrganizationId.Value != 0)
                query = query.Where(x => x.OrganizationId == searchModel.OrganizationId.Value);


            var organizations = _organizationContext.Organizations.Select(x => new { x.Id, x.NameEn }).ToList();
            
            var store = query.OrderByDescending(x => x.Id)
               .Include(s => s.StoreStockTypes)
                   .ThenInclude(sst => sst.StockType)
                .Select(x => new StoreViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    CityId = x.CityId,
                    StationId = x.StationId,
                    StockTypeId = x.DefaultStockTypeId,
                    StockType = x.DefaultStockType.Name,
                    StockTypes = x.StoreStockTypes.Select(sst => new StockTypeViewModel
                    {
                        Name = sst.StockType.Name,
                        Code = sst.StockType.Code
                    }).ToList(),
                    ParentId = x.ParentId,
                    //ProviderId = x.ProviderId 
                    IsActive = x.IsActive,
                    CreationDate = x.CreationDate.ToShortDateString(),
                    OrganizationId = x.OrganizationId
                }).ToList();

            //var store = query.OrderByDescending(x => x.Id).ToList();

            store.ForEach(item =>
            {
                string? nameEn = organizations.FirstOrDefault(x => x.Id == item.OrganizationId)?.NameEn;
                item.Name = nameEn;
            });

            return store.ToList();

        }

        public Store GetStoresBy(long organizationId)
        {
            return _partContext.Stores
                .Include(s => s.StoreStockTypes)
                    .ThenInclude(sst => sst.StockType)
                .FirstOrDefault(x => x.Id == organizationId);

            //var organizations = _organizationContext.Organizations.Select(x => new { x.Id, x.NameEn }).ToList();

            //var query = _partContext.Stores.Select(x => new StoreViewModel()
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    Code = x.Code,
            //    OrganizationId = x.OrganizationId,
            //    //CityId = x.CityId,
            //    //StationId = x.StationId,

            //    StockTypeId = x.StockTypeId,
            //    StockType = x.StockType.Name,
            //    //ParentId = x.ParentId,
            //    //ProviderId = x.ProviderId 
            //    IsActive = x.IsActive,
            //    CreationDate = x.CreationDate.ToString()

            //});

            //if (organizationId != 0)
            //    store = store.Where(x => x.OrganizationId == organizationId);

            //store.ForEach(item =>
            //{
            //    string? nameEn = organizations.FirstOrDefault(x => x.Id == item.OrganizationId)?.NameEn;
            //    item.Organization = nameEn;
            //});

            //return store.ToList();

        }
    }
}
