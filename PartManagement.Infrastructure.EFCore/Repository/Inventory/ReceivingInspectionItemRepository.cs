using _0_Framework.Infrastructure;
using PartManagement.Domain.InventoryAgg.ReceivingInspectionAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartManagement.Infrastructure.EFCore.Repository.Inventory
{
    public class ReceivingInspectionItemRepository
        : RepositoryBase<long, ReceivingInspectionItem>, IReceivingInspectionItemRepository
    {
        private readonly PartContext _context;

        public ReceivingInspectionItemRepository(PartContext context) : base(context)
        {
            _context = context;
        }

        public List<ReceivingInspectionItem> GetActiveOrdered(long? stockTypeId = null, string keyword = null)
        {
            var q = _context.ReceivingInspectionItems.Where(x => x.IsActive);

            if (stockTypeId.HasValue)
                q = q.Where(x => x.StockTypeId == null || x.StockTypeId == stockTypeId);

            if (!string.IsNullOrWhiteSpace(keyword))
                q = q.Where(x => x.ApplicableKeyword == null
                                 || x.ApplicableKeyword == keyword);

            return q.OrderBy(x => x.DisplayOrder).ThenBy(x => x.Code).ToList();
        }
    }
}
