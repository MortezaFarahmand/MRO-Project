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
                StockTypeId = x.StockTypeId,
                ParentId = x.ParentId,
                ProviderId = x.ProviderId

            }).FirstOrDefault(x => x.Id == id);
        }


        public List<StoreViewModel> Search(StoreSearchModel searchModel)
        {
            var organizations = _organizationContext.Organizations.Select(x => new { x.Id, x.NameEn }).ToList();
            var query = _partContext.Stores
                .Include(x => x.StockType)
                .Select(x => new StoreViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    OrganizationId = x.OrganizationId,
                    //CityId = x.CityId,
                    //StationId = x.StationId,

                    StockTypeId = x.StockTypeId,
                    StockType = x.StockType.Name,
                    //ParentId = x.ParentId,
                    //ProviderId = x.ProviderId 
                    IsActive = x.IsActive,
                    CreationDate = x.CreationDate.ToString()
                });

            if(!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name == searchModel.Name);

            if (!string.IsNullOrWhiteSpace(searchModel.Code))
                query = query.Where(x => x.Name == searchModel.Code);

            if (searchModel.StockTypeId != 0)
                query = query.Where(x => x.StockTypeId == searchModel.StockTypeId);

            var store = query.OrderByDescending(x => x.Id).ToList();

            store.ForEach(item => 
            {
                string? nameEn = organizations.FirstOrDefault(x => x.Id == item.OrganizationId)?.NameEn;
                item.Organization = nameEn;
            });

            return store.ToList();

        }

        public List<StoreViewModel> GetStoresByOrganizationId(StoreViewModel storeViewModel, long organizationId)
        {
            var organizations = _organizationContext.Organizations.Select(x => new { x.Id, x.NameEn }).ToList();

            var query = _partContext.Stores.Select(x => new StoreViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                OrganizationId = x.OrganizationId,
                //CityId = x.CityId,
                //StationId = x.StationId,

                StockTypeId = x.StockTypeId,
                StockType = x.StockType.Name,
                //ParentId = x.ParentId,
                //ProviderId = x.ProviderId 
                IsActive = x.IsActive,
                CreationDate = x.CreationDate.ToString()

            });

            if (organizationId != 0)
                query = query.Where(x => x.OrganizationId == organizationId);

            if (!string.IsNullOrWhiteSpace(storeViewModel.Name))
                query = query.Where(x => x.Name.Contains(storeViewModel.Name));
            
            
            var store = query.OrderByDescending(x => x.Id).ToList();

            store.ForEach(item =>
            {
                string? nameEn = organizations.FirstOrDefault(x => x.Id == item.OrganizationId)?.NameEn;
                item.Organization = nameEn;
            });

            return store.ToList();

        }
    }
}
