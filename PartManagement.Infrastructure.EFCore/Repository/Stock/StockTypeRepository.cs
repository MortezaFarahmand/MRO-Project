using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using OrganizationManagement.Domain.OrganizationAgg;
using PartManagement.Application.Contracts.Stock.StockType;
using PartManagement.Application.Contracts.Stock.Store;
using PartManagement.Domain.Stock.StockTypeAgg;
using PartManagement.Domain.Stock.StoreAgg;
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

        public List<StockTypeViewModel> GetStockTypesBy(long? organizationId)
        {
            var query = _partContext.StockTypes
                .Where(x => x.OrganizationId == organizationId)
                .Select(x => new StockTypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    ColorCode = x.ColorCode,
                    OrganizationId = organizationId
                    //Store = x.Store.Name,
                }).ToList();


            return query;
        }

        public List<StockTypeViewModel> Search(StockTypeSearchModel searchModel, long? organizationId)
        {
            searchModel ??= new StockTypeSearchModel();

            var query = _partContext.StockTypes.AsQueryable();

            // در این شرط در صورتی که آی دی سازمان اکانت وارد شده صفر باشد دسترسی به تمام نوع استاک هایی که آیدی آنها غیر صفر است دارد و
            // در غیر این صورت یعنی زمانی که آیدی سازمان اکانت وارد شده صفر نباشد فقط نوع استاک هایی را میتواند مشاهده کند که 
            // آیدی سازمان آن با آیدی سازمان انتخاب شده از سلکتور لیست نوع استاک ها یکی باشد.

            // اگه کاربر عادیه (سازمان داره) -> فقط سازمان خودش، سرچ نادیده گرفته میشه (مهم برای امنیت)
            if (organizationId != 0)
            {
                query = query.Where(x => x.OrganizationId == organizationId);
            }

            // اگه ادمینه (صفر) -> همه رو نشون بده، مگر اینکه خودش فیلتر انتخاب کرده باشه
            else
            {
                if (searchModel.OrganizationId.HasValue && searchModel.OrganizationId.Value != 0)
                    query = query.Where(x => x.OrganizationId == searchModel.OrganizationId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            //if (searchModel.StoreId != 0)
            //{
            //    // فقط استاک‌تایپ‌هایی که این استور دیفالتشونه یا توی رابطه چندبه‌چند دارنش
            //    query = query.Where(x =>
            //        x.Stores_DefaultStockType.Any(s => s.Id == searchModel.StoreId) ||
            //        x.StoreStockTypes.Any(ss => ss.StoreId == searchModel.StoreId)
            //    );
            //}

            // 2. بعد نمایش - آخرش Select
            return query.OrderByDescending(x => x.Id)
                .Select(x => new StockTypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name ?? "-",  // ?? "-" با این کد پراپرتی نال سیف میشه
                    Code = x.Code ?? "-",
                    ColorCode = x.ColorCode ?? "-",
                    CreationDate = x.CreationDate.ToShortDateString() ?? "-",
                    OrganizationId = x.OrganizationId ?? 0,

                    Store = x.Stores_DefaultStockType
                        .Select(s => new StoreViewModel()
                        {
                            Id = s.Id,
                            Name = s.Name ?? "-",
                            Code = s.Code ?? "-"
                        }).FirstOrDefault(),
                    // این خط جدیده:
                    StoreId = x.Stores_DefaultStockType
                    .Select(s => (long?)s.Id)
                    .FirstOrDefault() ?? 0

                }).ToList();

            // اگه استاک‌تایپ استور دیفالت داشته باشه استورآیدی برابر آیدی همون استور بعلاوه استوردات نیم هم پره
            //  اگه نداشته باشه استورآیدی برابر صفر بعلاوه ی استور مساوی نال میشه که توی ویو با @(item.Store?.Name ?? "-") نمایش میدی
        }

    }
}
