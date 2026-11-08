using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PartManagement.Domain.InventoryAgg.ReceivingInspectionAgg;
using PartManagement.Domain.InventoryAgg.ReceivingInspectionAgg.Enum;

namespace PartManagement.Infrastructure.EFCore.Repository.Inventory
{
    public class ReceivingInspectionResultRepository
        : RepositoryBase<long, ReceivingInspectionResult>, IReceivingInspectionResultRepository
    {
        private readonly PartContext _context;

        public ReceivingInspectionResultRepository(PartContext context) : base(context)
        {
            _context = context;
        }

        public ReceivingInspectionResult GetWithChecks(long id)
        {
            return _context.ReceivingInspectionResults
                .Include(x => x.Checks) // یا .Include(x => x.Checks) اگر Navigation عمومی map شده
                .FirstOrDefault(x => x.Id == id);
        }

        public List<ReceivingInspectionResult> GetByReceipt(long receiptId)
        {
            return _context.ReceivingInspectionResults
                .Where(x => x.ReceiptId == receiptId)
                .OrderByDescending(x => x.Id)
                .ToList();
        }

        public List<ReceivingInspectionResult> GetPendingByStore(long storeId)
        {
            return _context.ReceivingInspectionResults
                .Where(x => x.StoreId == storeId
                            && (x.Status == InspectionStatus.Pending
                                || x.Status == InspectionStatus.InProgress
                                || x.Status == InspectionStatus.NeedsMoreInspection))
                .OrderBy(x => x.DeadlineAt)
                .ToList();
        }
    }
}
